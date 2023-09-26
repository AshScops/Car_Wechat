using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;


namespace QFramework.Car
{
    public class PlayerPower
    {
        /// <summary>
        /// ��ǰ����Ч��PowerId
        /// </summary>
        private List<int> m_powerIds = new List<int>();

        /// <summary>
        /// ��ʼ����ʼ�������CommonPower����
        /// </summary>
        public List<PowerData> CommonPowerData = new List<PowerData>();

        /// <summary>
        ///  ��ʼ����ʼ�������SpecialPowers����
        /// </summary>
        public List<PowerData> SpecialPowerData = new List<PowerData>();

        /// <summary>
        /// ��ʼ����ʼ�����������Power��ʵ����
        /// </summary>
        private List<PowerBase> m_allPower = new List<PowerBase>();

        /// <summary>
        /// ÿ��������չʾ��Power����
        /// </summary>
        private int powerNumPerLevel = 5;

        public EasyEvent<int> GetPowerCnt = new EasyEvent<int>();

        #region ��ʼ��
        public void Init()
        {
            InitAllPowerData();
            InitAllPower();
        }

        /// <summary>
        /// ��ȡjson�ļ�
        /// </summary>
        private void InitAllPowerData()
        {
            ResUtil.LoadAssetAsync<TextAsset>("CommonPowers", (textAsset) =>
            {
                var json = textAsset.text;
                CommonPowerData = JsonConvert.DeserializeObject<List<PowerData>>(json);
                //Debug.Log(CommonPowerData.Count);
            });

            ResUtil.LoadAssetAsync<TextAsset>("SpecialPowers", (textAsset) =>
            {
                var json = textAsset.text;
                SpecialPowerData = JsonConvert.DeserializeObject<List<PowerData>>(json);
                //Debug.Log(SpecialPowerData.Count);
            });

        }

        #region OldFunction
        ///// <summary>
        ///// ��ȡstreamingAssest�ļ����µ�
        ///// </summary>
        ///// <param name="JsonName">json�ı�������</param>
        ///// <param name="action">�ص�������string  ����Ҫ��ֵ���ַ�����</param>
        //public void GetJsonText(string JsonName, Action<string> action)
        //{
        //            string path =
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //        Application.streamingAssetsPath + "/Json/modelname.json";
        //#elif UNITY_IPHONE && !UNITY_EDITOR
        //        "file://" + Application.streamingAssetsPath + "/Json/modelname.json";
        //#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
        //        "file://" + Application.streamingAssetsPath + "/powers.json";
        //#else
        //        string.Empty;
        //#endif
        //    //StartCoroutine(IGetText(JsonName + ".json", action));
        //}

        //IEnumerator IGetText(string JsonName, Action<string> action)
        //{
        //    UnityWebRequest www = UnityWebRequest.Get(new Uri(Path.Combine(Application.streamingAssetsPath, JsonName)));
        //    yield return www.SendWebRequest();
        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        Debug.LogError(www.error);
        //    }
        //    else
        //    {
        //        string str = www.downloadHandler.text;
        //        action?.Invoke(str);
        //        Debug.LogError("��ȡ�����ļ�" + str);
        //    }
        //}
        #endregion

        private void InitAllPower()
        {
            //��ȡPowerBase����������
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filterTypes = types.Where(type => type.IsSubclassOf(typeof(PowerBase)) && !type.ContainsGenericParameters && type.IsClass);
            var powerTypes = filterTypes.ToList();

            foreach (var type in powerTypes)
            {
                PowerBase power = Activator.CreateInstance(type) as PowerBase;
                m_allPower.Add(power);
            }
        }
        #endregion

        /// <summary>
        /// �ӳ��������ȡ������ܣ��������������ϴ����ȡǰ��������������ȡȫ��
        /// </summary>
        /// <param name="powerDatas"></param>
        /// <returns>���������Power����</returns>
        public Queue<PowerData> RandomChoosePower(List<PowerData> powerDatas)
        {
            List<PowerData> newPowerData = powerDatas.Where(data => !m_powerIds.Contains(data.PowerId)).ToList();
            RandomUtil.Shuffle(ref newPowerData);
            Queue<PowerData> queue;

            if (newPowerData.Count <= this.powerNumPerLevel)
            {
                queue = new Queue<PowerData>(newPowerData);
            }
            else
            {
                queue = new Queue<PowerData>(newPowerData.GetRange(0, this.powerNumPerLevel));
            }
            return queue;
        }

        public void Add(int powerId)
        {
            if (m_powerIds.Contains(powerId)) return;

            m_powerIds.Add(powerId);
            FindPower(powerId).OnAttach();

            GetPowerCnt.Trigger(m_powerIds.Count);
        }

        public void Remove(int powerId)
        {
            if (!m_powerIds.Contains(powerId)) return;

            FindPower(powerId).OnUnattach();
            m_powerIds.Remove(powerId);
        }

        public void RemoveAll()
        {
            foreach(var powerId in m_powerIds)
            {
                FindPower(powerId).OnUnattach();
            }

            m_powerIds.Clear();
        }

        /// <summary>
        ///  ������ѡPowerOnUpdate
        /// </summary>
        public void OnUpdate()
        {
            foreach(var powerId in m_powerIds)
            {
                FindPower(powerId).OnUpdate();
            }
        }

        private PowerBase FindPower(int powerId)
        {
            foreach (PowerBase power in m_allPower)
            {
                if(power.GetPowerId() == powerId)
                    return power;
            }

            Debug.LogError("δ�ҵ���Id��Ӧ��Power");
            return null;
        }


        /// <summary>
        /// �����Ƿ��Ƴ��ɹ�������Ԫ�����޷��Ƴ�����false
        /// </summary>
        /// <returns></returns>
        public bool RandomLostBuff()
        {
            //TODO:����Ƴ�һ��

            return m_powerIds.Count > 0;
        }


    }
}