using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace QFramework.Car
{
	public class UIRunningPanelData : UIPanelData
	{
	}
	public partial class UIRunningPanel : UIPanel
	{
		private PlayerModel m_playerModel;
        private ItemModel m_itemModel;
        private List<GameObject> m_hpList = new List<GameObject>();
		private int m_hpImgCnt = 0;

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIRunningPanelData ?? new UIRunningPanelData();
			m_playerModel = GameArch.Interface.GetModel<PlayerModel>();
            m_itemModel = GameArch.Interface.GetModel<ItemModel>();
            

            m_playerModel.MaxHp.RegisterWithInitValue((maxhp) =>
            {
				m_hpImgCnt = m_hpList.Count;

                while (m_hpImgCnt < maxhp)
                {
					m_hpImgCnt++;

                    ResUtil.GenerateGOAsync("HpGrid", HpRoot, (heart) =>
					{
                        m_hpList.Add(heart);
                    });
                }
            }).UnRegisterWhenGameObjectDestroyed(this);

            m_playerModel.Hp.RegisterWithInitValue((hp) =>
            {
				if (hp < 0) return;

                ResUtil.LoadTextureAsync("heart", (tex) =>
				{
                    Sprite heart = Sprite.Create(tex, new Rect(0f, 96f, 32f, 32f), new Vector2(0.5f, 0.5f));
                    Sprite greyHeart = Sprite.Create(tex, new Rect(64f, 0f, 32f, 32f), new Vector2(0.5f, 0.5f));

                    for (int i = 0; i < m_hpList.Count; i++)
                    {
                        if (i < hp)
                            m_hpList[i].GetComponent<Image>().sprite = heart;
                        else
                            m_hpList[i].GetComponent<Image>().sprite = greyHeart;
                    }
                });

            }).UnRegisterWhenGameObjectDestroyed(this);

            m_playerModel.CurrentExp.RegisterWithInitValue((exp) =>
			{
                //Debug.Log("exp:" + m_playerModel.CurrentExp + " limit:" + m_playerModel.LevelUpExpUpperLimit);
				float v = 1.0f * m_playerModel.CurrentExp / m_playerModel.LevelUpExpUpperLimit;
				v = v > 1f ? 1f : v;
				ExpSlider.value = v;
			}).UnRegisterWhenGameObjectDestroyed(this);


			m_itemModel.Coin.RegisterWithInitValue((coin) =>
			{
				CoinNum.text = coin.ToString();
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
