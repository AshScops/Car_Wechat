using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QFramework.Car
{
    public class PlayerWeapon
    {
        private readonly List<string> paths = new List<string>()
        {
            "Pistol",
            "Uzi",
            "Shotgun",
            "RocketLauncher"
        };

        private PlayerModel m_playerModel;
        private Dictionary<string, List<GameObject>> m_currentWeapons = new Dictionary<string, List<GameObject>>();
        public EasyEvent<string> WeaponFull = new EasyEvent<string>();

        public void Init()
        {
            m_playerModel = GameArch.Interface.GetModel<PlayerModel>();
        }

        /// <param name="path"></param>
        /// <returns>添加成功返回true，已满返回false</returns>
        public bool Add(int index)
        {
            return Add(paths[index]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weaponPrefab"></param>
        /// <returns>添加成功返回true，已满返回false</returns>
        public bool Add(string path)
        {
            bool res = false;

            var m_weaponRoot = m_playerModel.PlayerTrans.Find("WeaponRoot");
            for (int i = 0; i < m_weaponRoot.childCount; i++)
            {
                if(m_weaponRoot.GetChild(i).childCount == 0)
                {
                    ResUtil.GenerateGOAsync(path, m_weaponRoot.GetChild(i), (go) =>
                    {
                        if (!m_currentWeapons.ContainsKey(path))
                        {
                            m_currentWeapons[path] = new List<GameObject>();
                        }
                        m_currentWeapons[path].Add(go);

                        //检测栏位是否满
                        bool weaponFull = true;
                        for (int i = 0; i < m_weaponRoot.childCount; i++)
                        {
                            if (m_weaponRoot.GetChild(i).childCount == 0)
                            {
                                weaponFull = false;
                                break;
                            }
                        }
                        if (weaponFull)
                            WeaponFull.Trigger("装备武器至栏位上限");

                    });
                    res = true;
                    break;
                }
            }

            if(res == false)
                UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "武器栏位（为五）已满" });

            return res;
        }


        public void Remove(int index)
        {
            Remove(paths[index]);
        }

        public void Remove(string path)
        {
            if (m_currentWeapons.ContainsKey(path))
            {
                var delGo = m_currentWeapons[path][0];
                m_currentWeapons[path].RemoveAt(0);
                GameObject.Destroy(delGo);
                if (m_currentWeapons[path].Count == 0)
                {
                    m_currentWeapons[path] = null;
                    m_currentWeapons.Remove(path);
                }
            }
            else
            {
                Debug.LogWarning("已经卸下该武器:" + path);
            }
        }


    }
}