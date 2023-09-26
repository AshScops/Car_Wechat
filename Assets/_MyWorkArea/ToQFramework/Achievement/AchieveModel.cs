using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class AchieveModel : AbstractModel
    {
        public List<AchieveInfo> AchieveInfoList;

        protected override void OnInit()
        {
            ResUtil.LoadAssetAsync<TextAsset>("Achieve", (textAsset) =>
            {
                var json = textAsset.text;
                AchieveInfoList = JsonConvert.DeserializeObject<List<AchieveInfo>>(json);
                //Debug.Log(AchieveInfoList.Count);
            });
        }



    }

}
