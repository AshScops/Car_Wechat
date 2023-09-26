using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:17f76cdc-2749-464f-b10e-5999dc3f52ba
	public partial class UITipPanel
	{
		public const string Name = "UITipPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform TipBackground;
		[SerializeField]
		public UnityEngine.UI.Text TipText;
		
		private UITipPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			TipBackground = null;
			TipText = null;
			
			mData = null;
		}
		
		public UITipPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UITipPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UITipPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
