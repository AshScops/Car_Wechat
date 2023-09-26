using QFramework.Custom;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.WX
{
    public class ResLoadWayInWX : IResLoadWay
    {
        private Dictionary<string, AssetBundle> m_name2ab;

        public ResLoadWayInWX()
        {
            m_name2ab = new Dictionary<string, AssetBundle>();
        }


        /// <summary>
        /// 通过QFramework的自定义Res加载AB包并执行回调
        /// </summary>
        public void Load<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            var resLoader = ResLoader.Allocate();
            var resSearchKeys = ResSearchKeys.Allocate(assetName);
            var assetData = AssetBundleSettings.AssetBundleConfigFile.GetAssetData(resSearchKeys);//通过资源名查找对应AB包名
            resSearchKeys.Recycle2Cache();
            string abName = assetData.OwnerBundleName;

            //先查看内存，有就加载，没有就发起网络请求
            if (m_name2ab.ContainsKey(abName))
            {
                Debug.Log($"{abName}包中的{assetName}加载完成");
                callback(m_name2ab[abName].LoadAsset(assetName) as T);
                return;
            }

            // 添加到加载队列
            resLoader.Add2Load("myserver://" + abName, (succeed, res) =>
            {
                if (!succeed)
                {
                    Debug.LogWarning("异步加载远端资源失败：");
                    return;
                }

                Debug.Log($"{res.AssetName}包中的{assetName}加载完成");
                //Debug.Log(res.State);
                if (res.Asset is not AssetBundle)
                {
                    Debug.LogWarning("所加载资源并非AB包，Type：" + res.AssetType);
                    return;
                }

                AssetBundle ab = res.Asset as AssetBundle;
                callback(ab.LoadAsset(assetName) as T);
                m_name2ab[abName] = ab;
                //这里没进行Unload，直接常驻内存
                //更好的方案是尽早Unload，并自行管理资源引用计数
            });
            // 执行异步加载
            resLoader.LoadAsync();
        }
    }


}
