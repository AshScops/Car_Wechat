using QFramework.Car;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace QFramework.Custom
{
    public class MyServerRes : Res
    {
        private string mABName;
        private string[] mDependResList;

        public static MyServerRes Allocate(string bundleName)
        {
            MyServerRes res = SafeObjectPool<MyServerRes>.Instance.Allocate();
            if (res != null)
            {
                res.AssetName = bundleName;
                res.SetUrl(bundleName);
                res.InitDependAssetBundleName();
            }
            return res;
        }

        public void SetUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            mABName = url;
        }

        private void InitDependAssetBundleName()
        {
            var targetABName = mABName.Split("myserver://")[1];
            mDependResList = AssetBundleSettings.AssetBundleConfigFile.GetAllDependenciesByUrl(targetABName);
            if (mDependResList == null) return;

            for (int i = 0; i < mDependResList.Length; i++)
            {
                mDependResList[i] = "myserver://" + mDependResList[i];
                //Debug.Log("mDependResList[i]: " + mDependResList[i]);
            }
        }

        // WXminigame不支持磁盘同步加载，只支持按需下载至内存中随取随用
        public override bool LoadSync()
        {
            return false;
        }

        // 异步加载
        public override void LoadAsync()
        {
            if (!CheckLoadAble()) return;
            if (string.IsNullOrEmpty(mAssetName)) return;

            DoLoadWork();
        }

        private void DoLoadWork()
        {
            State = ResState.Loading;
            OnDownLoadResult(true);
        }

        private void OnDownLoadResult(bool result)
        {
            if (!result)
            {
                OnResLoadFaild();
                return;
            }

            if (RefCount <= 0)
            {
                State = ResState.Waiting;
                return;
            }

            ResMgr.Instance.PushIEnumeratorTask(this);
        }

        public override IEnumerator DoLoadAsync(System.Action finishCallback)
        {
            if (AssetBundlePathHelper.SimulationMode)
            {
                yield return null;
            }
            else
            {
                //向ABNameList中匹配前缀.StartsWith("myserver://");
                var targetABName = mABName.Split("myserver://")[1];
                var list = NetManager.ABNameList;
                var abNames = list.Where(name => name.StartsWith(targetABName)).ToList();
                if (abNames.Count == 0)
                    Debug.Log("没有找到符合该前缀的AB包名，前缀:" + targetABName);

                Debug.Log("开始下载targetABName:" + targetABName);

                UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(NetManager.remoteUrl + abNames.First());
                //yield return request.SendWebRequest();
                request.SendWebRequest();
                while (!request.isDone)
                {
                    LoadBarCanvas.ShowLoadProgress(request.downloadProgress, targetABName);
                    yield return 0;
                }

                if (!string.IsNullOrEmpty(request.error))
                {
                    Debug.LogError(request.error);
                }
                else
                {
                    AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                    mAsset = ab;
                    Debug.Log(ab.name + "AB包已常驻内存");
                    //ab.Unload(false);//这里暂时不用卸载，卸载放在外部业务回调结束后
                }
                request.Dispose();
            }

            State = ResState.Ready;
            finishCallback();
        }

        public override string[] GetDependResList()
        {
            return mDependResList;
        }


        // 释放资源（自己实现)
        //先不释放AB包，小游戏常驻内存就好
        //protected override void OnReleaseRes()
        //{
        //    // 卸载操作
        //    // Asset = null
        //    //释放AB包
        //    if (mAsset != null)
        //    {
        //        if (this.Asset is AssetBundle)
        //        {
        //            this.Asset.As<AssetBundle>().Unload(false);
        //        }
        //        mAsset = null;
        //    }

        //    State = ResState.Waiting;
        //}

        public override void Recycle2Cache()
        {
            SafeObjectPool<MyServerRes>.Instance.Recycle(this);
        }

        public override void OnRecycled()
        {

        }

    }

}