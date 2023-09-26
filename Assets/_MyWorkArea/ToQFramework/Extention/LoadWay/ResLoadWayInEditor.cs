using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Custom
{
    public class ResLoadWayInEditor : IResLoadWay
    {
        public void Load<T>(string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            ///开发时其实是同步加载的
            callback(ResLoader.Allocate().LoadSync<T>(assetName));
            return;
        }
    }
}