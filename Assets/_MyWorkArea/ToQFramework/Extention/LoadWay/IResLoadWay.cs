using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Custom
{
    public interface IResLoadWay
    {
        public void Load<T>(string assetName, Action<T> callback) where T : UnityEngine.Object;
    }
}