using QFramework.Car;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace QFramework.Custom
{
    /// <summary>
    /// 微信小游戏平台的要求是资源均从服务器的AB包中异步加载
    /// </summary>
    public class NetManager : MonoSingleton<NetManager> 
    {
        public const string remoteUrl = "https://ashscopsgameserver.fun/StreamingAssets/";
        public static List<string> ABNameList = new List<string>();

        public bool SimulateWXLoadWay = false;
        private static IResLoadWay m_resLoadWay;
        public static bool NetInitDone = false;

        private IEnumerator Start()
        {
            Debug.Log("NetManager Init");
            yield return ResKit.InitAsync();
            m_resLoadWay = new ResLoadWayWithResources();
            NetInitDone = true;
            GameController.Instance.Init();
        }

        /// <summary>
        /// 在业务上仅关注加载后逻辑即可
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static void LoadAsync<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            m_resLoadWay.Load<T>(assetName, callback);
        }

        #region 官方给出的加载示例
        /// <summary>
        /// 小游戏因其平台特殊性，需要保证加载速度，因此在底层对bundle文件做了缓存，开发者无须自己实现缓存。
        /// 游戏逻辑还是按照未缓存需要从网络下载去编写，插件底层会判断是否已有缓存。若未缓存则缓存此bundle；
        /// 若已缓存，则返回缓存文件，实际不会发起网络请求。
        /// 在业务侧看来：总是使用异步接口从远程下载并使用，底层资源的缓存与更新已由适配层自动完成，游戏不再直接读写文件系统。
        /// 微信小游戏 AssetBundle使用方式:
        /// 打包ab时文件名带hash-->UnityWebRequest按需下载并使用资源
        /// </summary>
        /// <param name="uriPath"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private IEnumerator LoadAssetBundleAsync(string uriPath, Action<AssetBundle> callback)
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uriPath);
            yield return request.SendWebRequest();
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
            }
            else
            {
                AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                // ab.LoadAsset
                callback(ab);

                ab.Unload(false);
            }
            request.Dispose();
        }
        #endregion

        private IEnumerator GetABNameList(Action callback = null)
        {
            yield return DownloadABNameList(remoteUrl + "ABNameList.txt", callback);
        }

        private IEnumerator DownloadABNameList(string uriPath, Action callback = null)
        {
            UnityWebRequest request = UnityWebRequest.Get(uriPath);
            yield return request.SendWebRequest();

            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.LogError(request.error);
            }
            else
            {
                string[] res = request.downloadHandler.text.Split("\n");
                for(int i = 0; i < res.Length;i++)
                {
                    NetManager.ABNameList.Add(res[i]);
                    print(res[i]);
                }
            }
            request.Dispose();

            if(callback != null)
                callback();
        }
    }

}