using QFramework.Custom;
using System;
using UnityEngine;

namespace QFramework.Car
{
    public class ResLoadWayWithResources : IResLoadWay
    {
        //直接从Resource中取
        public void Load<T>(string path, Action<T> callback) where T : UnityEngine.Object
        {
            T obj = Resources.Load<T>(path);
            callback(obj);
        }
    }

}
