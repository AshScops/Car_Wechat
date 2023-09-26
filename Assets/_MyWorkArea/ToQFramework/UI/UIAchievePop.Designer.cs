using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:aaf87fcb-a5b9-42f3-ad41-11d33853f3a2
	public partial class UIAchievePop
	{
		public const string Name = "UIAchievePop";
		
		[SerializeField]
		public UnityEngine.RectTransform AchievePop;
		[SerializeField]
		public UnityEngine.UI.Image AchieveImg;
		[SerializeField]
		public UnityEngine.UI.Text AchieveName;
		[SerializeField]
		public UnityEngine.UI.Text AchieveDesc;
		
		private UIAchievePopData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			AchievePop = null;
			AchieveImg = null;
			AchieveName = null;
			AchieveDesc = null;
			
			mData = null;
		}
		
		public UIAchievePopData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIAchievePopData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIAchievePopData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
