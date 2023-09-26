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
        /// ͨ��QFramework���Զ���Res����AB����ִ�лص�
        /// </summary>
        public void Load<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            var resLoader = ResLoader.Allocate();
            var resSearchKeys = ResSearchKeys.Allocate(assetName);
            var assetData = AssetBundleSettings.AssetBundleConfigFile.GetAssetData(resSearchKeys);//ͨ����Դ�����Ҷ�ӦAB����
            resSearchKeys.Recycle2Cache();
            string abName = assetData.OwnerBundleName;

            //�Ȳ鿴�ڴ棬�оͼ��أ�û�оͷ�����������
            if (m_name2ab.ContainsKey(abName))
            {
                Debug.Log($"{abName}���е�{assetName}�������");
                callback(m_name2ab[abName].LoadAsset(assetName) as T);
                return;
            }

            // ��ӵ����ض���
            resLoader.Add2Load("myserver://" + abName, (succeed, res) =>
            {
                if (!succeed)
                {
                    Debug.LogWarning("�첽����Զ����Դʧ�ܣ�");
                    return;
                }

                Debug.Log($"{res.AssetName}���е�{assetName}�������");
                //Debug.Log(res.State);
                if (res.Asset is not AssetBundle)
                {
                    Debug.LogWarning("��������Դ����AB����Type��" + res.AssetType);
                    return;
                }

                AssetBundle ab = res.Asset as AssetBundle;
                callback(ab.LoadAsset(assetName) as T);
                m_name2ab[abName] = ab;
                //����û����Unload��ֱ�ӳ�פ�ڴ�
                //���õķ����Ǿ���Unload�������й�����Դ���ü���
            });
            // ִ���첽����
            resLoader.LoadAsync();
        }
    }


}
