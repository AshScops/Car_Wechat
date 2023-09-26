using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:bf909e9f-82d3-4da1-95d2-d245d9c8b646
	public partial class StorePanel
	{
		public const string Name = "StorePanel";
		
		[SerializeField]
		public RectTransform PanelRoot;
		[SerializeField]
		public QFramework.Car.MyButton StoreBtn;
		[SerializeField]
		public UnityEngine.UI.Text CoinNum;
		[SerializeField]
		public UnityEngine.RectTransform StoreRoot;
		[SerializeField]
		public UnityEngine.RectTransform InventoryItemParent;
		[SerializeField]
		public UnityEngine.RectTransform EquippedItemParent;
		
		private StorePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			PanelRoot = null;
			StoreBtn = null;
			CoinNum = null;
			StoreRoot = null;
			InventoryItemParent = null;
			EquippedItemParent = null;
			
			mData = null;
		}
		
		public StorePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		StorePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new StorePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
