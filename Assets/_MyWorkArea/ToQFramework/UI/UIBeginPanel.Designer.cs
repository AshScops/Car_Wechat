using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:3d136119-fdd3-4a48-a9e7-39e8bdb2f82a
	public partial class UIBeginPanel
	{
		public const string Name = "UIBeginPanel";
		
		[SerializeField]
		public UnityEngine.UI.Text DiamondNum;
		[SerializeField]
		public QFramework.Car.MyButton PlayBtn;
		[SerializeField]
		public QFramework.Car.MyButton StoreBtn;
		[SerializeField]
		public QFramework.Car.MyButton AchievementsBtn;
		[SerializeField]
		public QFramework.Car.MyButton SettingBtn;
		
		private UIBeginPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			DiamondNum = null;
			PlayBtn = null;
			StoreBtn = null;
			AchievementsBtn = null;
			SettingBtn = null;
			
			mData = null;
		}
		
		public UIBeginPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIBeginPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIBeginPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
