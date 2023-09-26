using UnityEngine;
using UnityEngine.UI;
using QFramework;
using DG.Tweening;

namespace QFramework.Car
{
	public class UITipPanelData : UIPanelData
	{
		public string TipText;
	}
	public partial class UITipPanel : UIPanel
	{
		private bool m_showing = false;

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UITipPanelData ?? new UITipPanelData();
			TipBackground.anchoredPosition = new Vector2(TipBackground.anchoredPosition.x, 300f);
        }
		
		protected override void OnOpen(IUIData uiData = null)
		{
            mData = uiData as UITipPanelData ?? new UITipPanelData();

			TipText.text = mData.TipText;

			if (m_showing) return;

			m_showing = true;
            ActionKit.Sequence()
				.Custom((c) =>
				{
					c.OnStart(() =>
					{
						TipBackground.DOAnchorPosY(-90f, 0.5f).OnComplete(c.Finish);
					});
                })
				.Delay(0.5f)
                .Custom((c) =>
                {
                    c.OnStart(() =>
                    {
                        TipBackground.DOAnchorPosY(300f, 0.3f).OnComplete(c.Finish);
                    });
					c.OnFinish(() =>
					{
						m_showing = false;
                    });
                }).Start(GameController.Instance);
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
