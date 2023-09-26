using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// �������һ���������Ƴ�������
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