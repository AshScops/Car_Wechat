using UnityEngine;
using DG.Tweening;

namespace QFramework.Car
{
	public class UIBeginPanelData : UIPanelData
	{
	}
	public partial class UIBeginPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIBeginPanelData ?? new UIBeginPanelData();

            LoadBarCanvas.Hide();

            //��Canvas���������ζ����Ϸ��ʽ��ʼ����ʱ�ϱ�
            if(!PlatformCheck.IsEditor)
                WeChatWASM.WX.ReportGameStart();

            GameArch.Interface.GetModel<ItemModel>().Diamond.RegisterWithInitValue((diamondCnt) =>
            {
                DiamondNum.text = diamondCnt.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            PlayBtn.onClick.AddListener(() =>
            {
                PlayBtn.interactable = false;
                StoreBtn.interactable = false;
                AchievementsBtn.interactable = false;
                SettingBtn.interactable = false;

                ActionKit.Sequence()
						.Callback(() =>
						{
							//��ť��Ч
                            PlayBtn.transform.DOScale(1.3f, 0.3f)
                                            .SetEase(Ease.OutQuad)
                                            .SetLoops(2, LoopType.Yoyo);
                        })
						.Delay(0.15f)
						.Callback(() =>
                        {
							//���潥��+�رս���
                            ActionKit.Custom(c =>
                            {
                                c.OnStart(() => { this.GetComponent<CanvasGroup>().DOFade(0, 0.5f); });
                                c.OnFinish(() => { CloseSelf(); });

                            }).Start(GameController.Instance);

                        })
                        .Delay(0.1f)
                        .Callback(() =>
                        {
                            //��Ϸ��ʼ
                            GameArch.Interface.GetSystem<GameSystem>().GameStart();
                        })
                        .Start(GameController.Instance);

            });

            //���̵����
            StoreBtn.onClick.AddListener(() =>
            {
                UIKitExtension.OpenPanelAsync<UISkillTreePanel>();
            });

            //�ɾ����
            AchievementsBtn.onClick.AddListener(() =>
            {
                UIKitExtension.OpenPanelAsync<UIAchievePanel>();
            });

			//�����˺����֣�������Ч
            SettingBtn.onClick.AddListener(() =>
            {
                UIKitExtension.OpenPanelAsync<UISettingPanel>();
            });

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
