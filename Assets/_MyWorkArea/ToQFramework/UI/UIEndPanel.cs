using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace QFramework.Car
{
	public class UIEndPanelData : UIPanelData
	{
	}
	public partial class UIEndPanel : UIPanel
	{
		private GameModel m_gameModel;
        private PlayerModel m_playerModel;

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIEndPanelData ?? new UIEndPanelData();
			m_gameModel = GameArch.Interface.GetModel<GameModel>();
            m_playerModel = GameArch.Interface.GetModel<PlayerModel>();

            var score = m_gameModel.Score;
            var level = m_playerModel.CurrentLevel;
            var sumDmg = m_gameModel.SumDmg;
            var diamonds = score / 10;

            Score.text = "得分：    " + score;
            Level.text = "等级：    " + level;
            SumDmg.text = "总伤害：    " + sumDmg;
            Diamond.text = "获得钻石：    " + diamonds; 

            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;

            Score.gameObject.SetActive(false);
            Level.gameObject.SetActive(false);
            SumDmg.gameObject.SetActive(false);
            Diamond.gameObject.SetActive(false);
            OkBtn.gameObject.SetActive(false);

            var duration = 0.4f;
            var shakeDuration = 0.2f;
            var shakeStrength = new Vector3(20f, 20f, 0);
            ActionKit.Sequence()
                .Callback(() =>
				{
                    AudioKit.PlaySound("You Lose (4)");
                    canvasGroup.DOFade(1, 1.5f);
                })
                .Delay(1.5f)
                .Callback(() =>
				{
                    DoAnim(Score.gameObject, shakeDuration, shakeStrength);
                })
				.Delay(duration)
				.Callback(() =>
				{
                    DoAnim(Level.gameObject, shakeDuration, shakeStrength);
                })
                .Delay(duration)
				.Callback(() =>
				{
                    DoAnim(SumDmg.gameObject, shakeDuration, shakeStrength);
                })
                .Delay(duration)
                .Callback(() =>
                {
                    DoAnim(Diamond.gameObject, shakeDuration, shakeStrength);
                })
                .Delay(duration)
                .Callback(() =>
                {
                    OkBtn.gameObject.SetActive(true);
                })
                .Start(GameController.Instance);


            OkBtn.onClick.AddListener(() =>
			{
                ActionKit.Sequence()
                    .Callback(() =>
                    {
                        UIKit.CloseAllPanel();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    })
					.DelayFrame(1)
					.Callback(() =>
					{
                        m_gameModel.ResetAllValue.Trigger();//重置所有脏数据
						Debug.Log("重置所有脏数据");
					})
					.Start(GameController.Instance);
            });
        }
		
        private void DoAnim(GameObject go, float shakeDuration, Vector3 shakeStrength)
        {
            go.SetActive(true);

            ActionKit.Custom(c =>
            {
                c.OnStart(() => go.transform.DOScale(3f, 0.2f).SetEase(Ease.OutQuad).From().OnComplete(c.Finish));
                c.OnFinish(() => go.transform.DOShakePosition(shakeDuration, shakeStrength));
            }).Start(GameController.Instance);
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
