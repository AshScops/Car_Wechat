using System;
using UnityEngine;

namespace QFramework.Custom
{
    public class ResLoadWayDefault : IResLoadWay
    {
        public void Load<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            var resLoader = ResLoader.Allocate();

            // 添加到加载队列
            resLoader.Add2Load(assetName, (succeed, res) =>
            {
                if (!succeed)
                {
                    Debug.LogWarning("异步加载资源失败：");
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