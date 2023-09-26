using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// 随机增加一把武器，移除不撤销
    /// </summary>
    public class GetExtraRandomWeapon : PowerBase
    {
        public override int GetPowerId()
        {
            return 5;
        }

        public override void OnAttach()
        {
            var itemModel = GameArch.Interface.GetModel<ItemModel>();

            float[] probs = Enumerable.Repeat(1f, itemModel.ItemDataList.Count).ToArray();
            int i = RandomUtil.RandomChoose(probs);
            itemModel.EquipItem(i);
        }


        public override void OnUnattach()
        {
            
        }
    }
}