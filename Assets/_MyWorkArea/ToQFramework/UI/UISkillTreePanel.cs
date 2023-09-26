using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Car
{
	public class UISkillTreePanelData : UIPanelData
	{
	}
	public partial class UISkillTreePanel : UIPanel
	{
		public Dictionary<Skill_SO, Button> SkillInfos = new Dictionary<Skill_SO, Button>();

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISkillTreePanelData ?? new UISkillTreePanelData();

            GameArch.Interface.GetModel<ItemModel>().Diamond.RegisterWithInitValue((diamondCnt) =>
            {
                DiamondNum.text = diamondCnt.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            foreach (var pair in SkillInfos)
            {
                pair.Value.onClick.AddListener(() =>
                {
                    if (Tip.gameObject.activeInHierarchy)
                    {
                        Tip.gameObject.SetActive(false);
                        SkillInfoPanel.gameObject.SetActive(true);
                    }

                    SkillImg.sprite = pair.Key.Img;
                    SkillName.text = pair.Key.Name;
                    SkillCost.text = pair.Key.Cost.ToString();
                    SkillDesc.text = pair.Key.Description;
                });
            }

			CloseBtn.onClick.AddListener(() =>
			{
				this.Hide();
			});
        }
		
		protected override void OnOpen(IUIData uiData = null)
		{
            SkillInfoPanel.gameObject.SetActive(false);
            Tip.gameObject.SetActive(true);
        }
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
