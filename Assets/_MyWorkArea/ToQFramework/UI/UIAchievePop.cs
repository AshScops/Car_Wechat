using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace QFramework.Car
{
	public class UIAchievePopData : UIPanelData
	{
	}
	public partial class UIAchievePop : UIPanel
	{
        private AchieveSystem m_achieveSystem;

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIAchievePopData ?? new UIAchievePopData();
            // please add init code here

            m_achieveSystem = GameArch.Interface.GetSystem<AchieveSystem>();

            m_achieveSystem.ShowAchieveUnlock.Register((achieveInfo) =>
            {
                Debug.Log(achieveInfo.AchieveName + achieveInfo.AchieveDesc);

                AchieveName.text = achieveInfo.AchieveName;
                AchieveDesc.text = achieveInfo.AchieveDesc;
                ResUtil.LoadTextureAsync(achieveInfo.AchieveImg, (tex) =>
                {
                    Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                    AchieveImg.sprite = sprite;
                });


                var duration = 3f;
                ActionKit.Sequence()
                .Callback(() =>
                {
                    AchievePop.DOAnchorPos(new Vector2(0, 0), 0.3f);
                })
                .Delay(duration)
                .Callback(() =>
                {
                    AchievePop.DOAnchorPos(new Vector2(0, -130f), 0.3f);
                })
                .Start(GameController.Instance);

            }).UnRegisterWhenGameObjectDestroyed(this);

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
