using QFramework.Car;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace QFramework.Custom
{
    /// <summary>
    /// ΢��С��Ϸƽ̨��Ҫ������Դ���ӷ�������AB�����첽����
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
        /// ��ҵ���Ͻ���ע���غ��߼�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static void LoadAsync<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            m_resLoadWay.Load<T>(assetName, callback);
        }

        #region �ٷ������ļ���ʾ��
        /// <summary>
        /// С��Ϸ����ƽ̨�����ԣ���Ҫ��֤�����ٶȣ�����ڵײ��bundle�ļ����˻��棬�����������Լ�ʵ�ֻ��档
        /// ��Ϸ�߼����ǰ���δ������Ҫ����������ȥ��д������ײ���ж��Ƿ����л��档��δ�����򻺴��bundle��
        /// ���ѻ��棬�򷵻ػ����ļ���ʵ�ʲ��ᷢ����������
        /// ��ҵ��࿴��������ʹ���첽�ӿڴ�Զ�����ز�ʹ�ã��ײ���Դ�Ļ������������������Զ���ɣ���Ϸ����ֱ�Ӷ�д�ļ�ϵͳ��
        /// ΢��С��Ϸ AssetBundleʹ�÷�ʽ:
        /// ���abʱ�ļ�����hash-->UnityWebRequest�������ز�ʹ����Դ
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