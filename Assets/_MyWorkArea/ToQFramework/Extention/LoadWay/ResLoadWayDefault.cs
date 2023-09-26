using System;
using UnityEngine;

namespace QFramework.Custom
{
    public class ResLoadWayDefault : IResLoadWay
    {
        public void Load<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            var resLoader = ResLoader.Allocate();

            // ��ӵ����ض���
            resLoader.Add2Load(assetName, (succeed, res) =>
            {
                if (!succeed)
                {
                    Debug.LogWarning("�첽������Դʧ�ܣ�");
                    return;
                }

                Debug.Log(res.AssetName);
                Debug.Log(res.State);

                callback(res.Asset.As<T>());
            });

            resLoader.LoadAsync();
        }
    }
}