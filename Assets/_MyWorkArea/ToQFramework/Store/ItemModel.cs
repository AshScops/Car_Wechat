using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QFramework.Car
{
    public class ItemModel : AbstractModel
    {
        public BindableProperty<int> Diamond;
        public BindableProperty<int> Coin;

        /// <summary>
        /// 开始即初始化，存放Item数据
        /// </summary>
        public List<ItemData> ItemDataList;

        public Dictionary<ItemData, BindableProperty<int>> InInventoryItem;
        public EasyEvent<ItemData, BindableProperty<int>> UpdateItemInInventoryEvent;

        public List<ItemData> EquippedItem;
        public EasyEvent<ItemData> UpdateItemEquipped;
        public EasyEvent<ItemData> UpdateItemUnEquipped;

        protected override void OnInit()
        {
            Diamond = new BindableProperty<int>(10000);
            Coin = new BindableProperty<int>(0);

            ItemDataList = new List<ItemData>();
            InInventoryItem = new Dictionary<ItemData, BindableProperty<int>>();
            EquippedItem = new List<ItemData>();

            UpdateItemInInventoryEvent = new EasyEvent<ItemData, BindableProperty<int>>();
            UpdateItemEquipped = new EasyEvent<ItemData>();
            UpdateItemUnEquipped = new EasyEvent<ItemData>();


            ResUtil.LoadAssetAsync<TextAsset>("Items", (textAsset) =>
            {
                var json = textAsset.text;
                ItemDataList = JsonConvert.DeserializeObject<List<ItemData>>(json);
                //Debug.Log(ItemDataList.Count);
            });


            GameArch.Interface.GetModel<GameModel>().ResetAllValue.Register(() =>
            {
                Coin.Value = 0;
            });
        }

        private ItemData GetItemData(int index)
        {
            var items = ItemDataList.ToArray().Where(item => index == item.ItemId).ToList();
            if (items == null || items.Count == 0) return null;
            return items[0];
        }

        /// <summary>
        /// 添加物品到背包
        /// </summary>
        /// <param name="item"></param>
        public void AddItemInInventory(ItemData item)
        {
            if(!InInventoryItem.ContainsKey(item))
                InInventoryItem.Add(item, new BindableProperty<int>(0));
            InInventoryItem[item].Value++;

            UpdateItemInInventoryEvent.Trigger(item, InInventoryItem[item]);
        }

        /// <summary>
        /// 从背包移除物品
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void RemoveItemInInventory(ItemData item)
        {
            if (!InInventoryItem.ContainsKey(item)) return;

            InInventoryItem[item].Value--;

            int res = InInventoryItem[item];

            UpdateItemInInventoryEvent.Trigger(item, InInventoryItem[item]);

            if (InInventoryItem[item] == 0)
                InInventoryItem.Remove(item);
        }

        /// <summary>
        /// 记录已装备的ItemData
        /// </summary>
        /// <param name="index">配置表中的ItemId</param>
        public void EquipItem(int index)
        {
            EquipItem(GetItemData(index));
        }

        public void EquipItem(ItemData item)
        {
            UpdateItemEquipped.Trigger(item);
        }

        public void UnEquipItem(int index)
        {
            UnEquipItem(GetItemData(index));
        }

        public void UnEquipItem(ItemData item)
        {
            EquippedItem.Remove(item);
            UpdateItemUnEquipped.Trigger(item);
        }

    }
}