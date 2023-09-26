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
        /// �ڴ�ע���¼�
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

                //��̫С�ˣ��ֻ���Ļ���ŷѾ�����ʱ����ʾ����
                //buff.OnBuffOverlay.Register(() =>
                //{
                //    int cnt = buff.GetEffectCnt();
                //    buffUIs[buff.GetType()] = 
                //});

                //����������Ч��Ӧ���ڴ�ִ�У�Ҫ�·ŵ�ʵ������
                //buff.OnBuffEffect.Register(() =>
                //{
                //    
                //});

                buff.OnBuffDestroy.Register(() =>
                {
                    //�Ƴ�UI
                    var GO = buffUIs[buff.GetType()];
                    GameObject.Destroy(GO);
                    buffUIs.Remove(buff.GetType());
                });
            }).UnRegisterWhenGameObjectDestroyed(targetGO);

        }


    }
}