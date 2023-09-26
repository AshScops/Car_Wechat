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
        /// 当前已生效的PowerId
        /// </summary>
        private List<int> m_powerIds = new List<int>();

        /// <summary>
        /// 开始即初始化，存放CommonPower数据
        /// </summary>
        public List<PowerData> CommonPowerData = new List<PowerData>();

        /// <summary>
        ///  开始即初始化，存放SpecialPowers数据
        /// </summary>
        public List<PowerData> SpecialPowerData = new List<PowerData>();

        /// <summary>
        /// 开始即初始化，存放所有Power的实现类
        /// </summary>
        private List<PowerBase> m_allPower = new List<PowerBase>();

        /// <summary>
        /// 每次升级后展示的Power数量
        /// </summary>
        private int powerNumPerLevel = 5;

        public EasyEvent<int> GetPowerCnt = new EasyEvent<int>();

        #region 初始化
        public void Init()
        {
            InitAllPowerData();
            InitAllPower();
        }

        /// <summary>
        /// 读取json文件
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
        ///// 读取streamingAssest文件夹下的
        ///// </summary>
        ///// <param name="JsonName">json文本的名字</param>
        ///// <param name="action">回调方法（string  是需要赋值的字符串）</param>
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
        //        Debug.LogError("读取到的文件" + str);
        //    }
        //}
        #endregion

        private void InitAllPower()
        {
            //获取PowerBase的所有子类
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
        /// 从池内随机抽取五个技能，具体做法是随机洗牌再取前五个，不足五个则取全部
        /// </summary>
        /// <param name="powerDatas"></param>
        /// <returns>返回所需的Power队列</returns>
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
        ///  所有已选PowerOnUpdate
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

            Debug.LogError("未找到该Id对应的Power");
            return null;
        }


        /// <summary>
        /// 返回是否移除成功，若无元素则无法移除返回false
        /// </summary>
        /// <returns></returns>
        public bool RandomLostBuff()
        {
            //TODO:随机移除一个

            return m_powerIds.Count > 0;
        }


    }
}