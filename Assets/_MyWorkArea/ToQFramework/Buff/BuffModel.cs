using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class BuffModel : AbstractModel
    {
        /// <summary>
        /// 开始即初始化，存放CommonPower数据
        /// </summary>
        private List<BuffData> BuffData;

        protected override void OnInit()
        {
            ResUtil.LoadAssetAsync<TextAsset>("Buffs", (textAsset) =>
            {
                var json = textAsset.text;
                BuffData = JsonConvert.DeserializeObject<List<BuffData>>(json);
                Debug.Log(BuffData.Count);
            });
        }


        public BuffData GetBuffData(int buffId)
        {
            for (int i = 0; i < BuffData.Count; i++)
            {
                if (BuffData[i].BuffId == buffId)
                    return BuffData[i];
            }
            return null;
        }
    }
}