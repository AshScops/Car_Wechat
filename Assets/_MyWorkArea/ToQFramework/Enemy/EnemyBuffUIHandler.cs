using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Car
{
    public class EnemyBuffUIHandler
    {
        public EnemyBuffUIHandler(GameObject targetGO, BuffHandler buffHandler)
        {
            m_uiParent = targetGO.transform.Find("UIParent");
            m_buffHandler = buffHandler;
            buffModel = BuffArch.Interface.GetModel<BuffModel>();
            Init(targetGO);
        }

        private Transform m_uiParent;
        private BuffHandler m_buffHandler;
        private BuffModel buffModel;
        private Dictionary<Type, GameObject> buffUIs = new Dictionary<Type, GameObject>();


        /// <summary>
        /// 在此注册事件
        /// </summary>
        private void Init(GameObject targetGO)
        {
            m_buffHandler.OnBuffHandlerAdd.Register((buff) =>
            {
                int cnt = buff.GetEffectCnt();
                ResUtil.GenerateGOAsync("BuffGrid", m_uiParent, (grid) =>
                {
                    string buffImgPath = buffModel.GetBuffData(buff.GetBuffId()).BuffImg;
                    ResUtil.LoadTextureAsync(buffImgPath, (tex) =>
                    {
                        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                        grid.GetComponent<Image>().sprite = sprite;
                    });
                    buffUIs[buff.GetType()] = grid;
                });

                //字太小了，手机屏幕看着费劲，暂时不显示层数
                //buff.OnBuffOverlay.Register(() =>
                //{
                //    int cnt = buff.GetEffectCnt();
                //    buffUIs[buff.GetType()] = 
                //});

                //播放受伤特效不应该在此执行，要下放到实现类中
                //buff.OnBuffEffect.Register(() =>
                //{
                //    
                //});

                buff.OnBuffDestroy.Register(() =>
                {
                    //移除UI
                    var GO = buffUIs[buff.GetType()];
                    GameObject.Destroy(GO);
                    buffUIs.Remove(buff.GetType());
                });
            }).UnRegisterWhenGameObjectDestroyed(targetGO);

        }


    }
}