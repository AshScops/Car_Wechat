using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Car
{
	// Generate Id:e6112a15-bebd-4a57-9498-68cfbee4c8bc
	public partial class UISkillTreePanel
	{
		public const string Name = "UISkillTreePanel";
		
		[SerializeField]
		public UnityEngine.UI.Text DiamondNum;
		[SerializeField]
		public UnityEngine.RectTransform SkillInfoPanel;
		[SerializeField]
		public UnityEngine.UI.Image SkillImg;
		[SerializeField]
		public UnityEngine.UI.Text SkillName;
		[SerializeField]
		public UnityEngine.UI.Text SkillCost;
		[SerializeField]
		public UnityEngine.UI.Text SkillDesc;
		[SerializeField]
		public UnityEngine.UI.Text Tip;
		[SerializeField]
		public QFramework.Car.MyButton CloseBtn;
		[SerializeField]
		public UnityEngine.UI.Button SkillBtn;
		[SerializeField]
		public UnityEngine.UI.Text DiamondCost;
		
		private UISkillTreePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			DiamondNum = null;
			SkillInfoPanel = null;
			SkillImg = null;
			SkillName = null;
			SkillCost = null;
			SkillDesc = null;
			Tip = null;
			CloseBtn = null;
			SkillBtn = null;
			DiamondCost = null;
			
			mData = null;
		}
		
		public UISkillTreePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UISkillTreePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UISkillTreePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
