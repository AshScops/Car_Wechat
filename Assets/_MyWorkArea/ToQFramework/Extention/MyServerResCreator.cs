using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Custom
{
    public class MyServerResCreator : IResCreator
    {
        public bool Match(ResSearchKeys resSearchKeys)
        {
            //Debug.Log("MyServerResCreator Match:" + resSearchKeys.AssetName.StartsWith("https://"));
            return resSearchKeys.AssetName.StartsWith("myserver://");
        }

        public IRes Create(ResSearchKeys resSearchKeys)
        {
            return MyServerRes.Allocate(resSearchKeys.OriginalAssetName);//这里加载的是AB包，仅需AB包名即可
        }
    }

}
