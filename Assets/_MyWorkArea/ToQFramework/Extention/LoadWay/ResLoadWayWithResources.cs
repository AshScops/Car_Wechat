using QFramework.Custom;
using System;
using UnityEngine;

namespace QFramework.Car
{
    public class ResLoadWayWithResources : IResLoadWay
    {
        //ֱ�Ӵ�Resource��ȡ
        public void Load<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            T obj = Resources.Load<T>(path);
            callback(obj);
        }
    }

}
