using Newtonsoft.Json;
using QFramework.Custom;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class JsonUtil
    {

        //public static void InitJsonData<T>(string fileName, ref List<T> list)
        //{
        //    TextAsset jsonFile = ResLoader.Allocate().LoadSync<TextAsset>(fileName);
        //    InitJsonData(jsonFile, list);
        //}


        public static void InitJsonDataAsync<T>(string fileName,  List<T> list)
        {
            NetManager.LoadAsync<TextAsset>(fileName, (jsonFile) =>
            {
                var json = jsonFile.text;
                list = JsonConvert.DeserializeObject<List<T>>(json);
                Debug.Log(list.Count);
            });
        }

        //public static void InitJsonData<T>(TextAsset jsonFile, out List<T> list)
        //{
        //    var json = jsonFile.text;

        //    //JsonUtility不能解析数组
        //    //m_allPowerData = JsonUtility.FromJson<List<PowerData>>(powerJson);
        //    list = JsonConvert.DeserializeObject<List<T>>(json);
        //    Debug.Log(list.Count);
        //}
    }
}