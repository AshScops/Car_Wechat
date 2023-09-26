using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:4cfc29d3-6b3f-4869-8657-4a9f992b6841
	public partial class ChoosePowerUI
	{
		public const string Name = "ChoosePowerUI";
		
		[SerializeField]
		public UnityEngine.CanvasGroup PanelRoot;
		[SerializeField]
		public RectTransform CommonPowerBtnParent;
		[SerializeField]
		public RectTransform SpecialPowerBtnParent;
		[SerializeField]
		public UnityEngine.UI.Text PowerDesc;
		[SerializeField]
		public QFramework.Car.MyButton ConfirmBtn;
		[SerializeField]
		public UnityEngine.UI.Text CommonPowerTitle;
		[SerializeField]
		public UnityEngine.UI.Text SpecialPowerTitle;
		[SerializeField]
		public UnityEngine.RectTransform LevelUpDuration;
		
		private ChoosePowerUIData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			PanelRoot = null;
			CommonPowerBtnParent = null;
			SpecialPowerBtnParent = null;
			PowerDesc = null;
			ConfirmBtn = null;
			CommonPowerTitle = null;
			SpecialPowerTitle = null;
			LevelUpDuration = null;
			
			mData = null;
		}
		
		public ChoosePowerUIData Data
		{
			get
			{
				return mData;
			}
		}
		
		ChoosePowerUIData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new ChoosePowerUIData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
