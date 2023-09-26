using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:d04bc3fe-2955-4c35-8a61-4d7886d6e36a
	public partial class UIEndPanel
	{
		public const string Name = "UIEndPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform EndPanelRoot;
		[SerializeField]
		public UnityEngine.RectTransform Title;
		[SerializeField]
		public UnityEngine.UI.Text Score;
		[SerializeField]
		public UnityEngine.UI.Text Level;
		[SerializeField]
		public UnityEngine.UI.Text SumDmg;
		[SerializeField]
		public UnityEngine.UI.Text Diamond;
		[SerializeField]
		public QFramework.Car.MyButton OkBtn;
		
		private UIEndPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			EndPanelRoot = null;
			Title = null;
			Score = null;
			Level = null;
			SumDmg = null;
			Diamond = null;
			OkBtn = null;
			
			mData = null;
		}
		
		public UIEndPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIEndPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIEndPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
