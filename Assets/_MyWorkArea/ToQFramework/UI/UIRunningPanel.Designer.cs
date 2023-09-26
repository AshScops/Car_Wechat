using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:a4d75d83-5427-4079-a30f-2e3790266591
	public partial class UIRunningPanel
	{
		public const string Name = "UIRunningPanel";
		
		[SerializeField]
		public RectTransform HpRoot;
		[SerializeField]
		public UnityEngine.UI.Text CoinNum;
		[SerializeField]
		public UnityEngine.UI.Slider ExpSlider;
		
		private UIRunningPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			HpRoot = null;
			CoinNum = null;
			ExpSlider = null;
			
			mData = null;
		}
		
		public UIRunningPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIRunningPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIRunningPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
