using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QFramework.Car
{
	public class UIAchievePanelData : UIPanelData
	{
	}
	public partial class UIAchievePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIAchievePanelData ?? new UIAchievePanelData();
			// please add init code here

            //首次打开，初始化所有元素
            var achieveSystem = GameArch.Interface.GetSystem<AchieveSystem>();
            var achieveModel = GameArch.Interface.GetModel<AchieveModel>();
			var achieveInfoList = achieveModel.AchieveInfoList;

			var frontQueue = new Queue<AchieveInfo>();
            var middleQueue = new Queue<AchieveInfo>();
            var backQueue = new Queue<AchieveInfo>();

			for(int i = 0; i < achieveInfoList.Count; i++)
			{
				//向AchieveSystem查询解锁和领取情况
				if(achieveSystem.AchieveUnlocked(achieveInfoList[i].AchieveId))
				{
					if (achieveSystem.AchieveBonusGot(achieveInfoList[i].AchieveId))
					{
                        //排最末
                        backQueue.Enqueue(achieveInfoList[i]);
                    }
					else
					{
                        //排最前
                        frontQueue.Enqueue(achieveInfoList[i]);
                    }
				}
				else
				{
                    middleQueue.Enqueue(achieveInfoList[i]);
                }
            }

			GenerateAchieveElement(frontQueue, Content, AchieveState.unlocked);
            GenerateAchieveElement(middleQueue, Content, AchieveState.locked);
            GenerateAchieveElement(backQueue, Content, AchieveState.gotbonus);

            CloseBtn.onClick.AddListener(() =>
            {
                this.Hide();
            });
        }

		private void GenerateAchieveElement(Queue<AchieveInfo> queue, Transform parent, AchieveState state)
		{
			if (queue == null)
			{
				Debug.Log("成就信息队列为空");
				return;
			}

            while(queue.Count != 0)
			{
				var achieveInfo = queue.Dequeue();

                ResUtil.GenerateGOAsync("SingleAchieve", parent, (singleAchieve) =>
				{
                    singleAchieve.transform.Find("AchieveName").GetComponent<Text>().text = achieveInfo.AchieveName;
                    singleAchieve.transform.Find("AchieveDesc").GetComponent<Text>().text = achieveInfo.AchieveDesc;
                    ResUtil.LoadTextureAsync(achieveInfo.AchieveImg, (tex) =>
                    {
                        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                        singleAchieve.transform.Find("AchieveImg").GetComponent<Image>().sprite = sprite;
                    });
                    singleAchieve.transform.Find("BonusText").GetComponent<Text>().text = achieveInfo.AchieveBonus.ToString();

                    var btn = singleAchieve.transform.Find("BonusBtn").GetComponent<Button>();
                    var btnText = btn.transform.Find("BonusBtnText").GetComponent<Text>();

                    if (state == AchieveState.gotbonus)
                    {
                        btnText.text = "已领取";
                        btn.interactable = false;
                    }
                    else if (state == AchieveState.locked)
                    {
                        btnText.text = "未解锁";
                        btn.interactable = false;
                    }
                    else if (state == AchieveState.unlocked)
                    {
                        btnText.text = "领取";

                        //领取后排至队尾
                        btn.onClick.AddListener(() =>
                        {
                            btn.interactable = false;
                            btnText.text = "已领取";
                            singleAchieve.transform.SetAsLastSibling();

                            GameArch.Interface.GetSystem<AchieveSystem>().AddBonusGot(achieveInfo.AchieveId);

                            //增加货币
                            GameArch.Interface.GetModel<ItemModel>().Diamond.Value += achieveInfo.AchieveBonus;
                        });
                    }
                });
            }
        }

		private enum AchieveState
		{
			locked = 0,
			unlocked,
			gotbonus
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
            
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
