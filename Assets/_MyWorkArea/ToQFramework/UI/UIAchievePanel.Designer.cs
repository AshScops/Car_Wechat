using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:24210a05-cb56-492f-b4d3-baa017d87732
	public partial class UIAchievePanel
	{
		public const string Name = "UIAchievePanel";
		
		[SerializeField]
		public RectTransform Content;
		[SerializeField]
		public QFramework.Car.MyButton CloseBtn;
		
		private UIAchievePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			CloseBtn = null;
			
			mData = null;
		}
		
		public UIAchievePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIAchievePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIAchievePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
