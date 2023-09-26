using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:2d4fdac7-6d7c-492c-b989-290e151a4fe4
	public partial class UISettingPanel
	{
		public const string Name = "UISettingPanel";
		
		[SerializeField]
		public QFramework.Car.MyButton Btn1;
		[SerializeField]
		public QFramework.Car.MyButton Btn2;
		[SerializeField]
		public QFramework.Car.MyButton CloseBtn;
		
		private UISettingPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Btn1 = null;
			Btn2 = null;
			CloseBtn = null;
			
			mData = null;
		}
		
		public UISettingPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISettingPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISettingPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
