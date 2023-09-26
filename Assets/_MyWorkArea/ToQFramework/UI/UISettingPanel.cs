using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Car
{
	public class UISettingPanelData : UIPanelData
	{
	}
	public partial class UISettingPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UISettingPanelData ?? new UISettingPanelData();

			var settingModel = GameArch.Interface.GetModel<SettingModel>();
            ChangeBtnImg(Btn1, settingModel.DmgNumEnable);
            ChangeBtnImg(Btn2, settingModel.AudioEnable);

            Btn1.onClick.AddListener(() =>
			{
				settingModel.DmgNumEnable.Value = !settingModel.DmgNumEnable;
				ChangeBtnImg(Btn1, settingModel.DmgNumEnable);
            });

            Btn2.onClick.AddListener(() =>
            {
                settingModel.AudioEnable.Value = !settingModel.AudioEnable;
                ChangeBtnImg(Btn2, settingModel.AudioEnable);
            });

			CloseBtn.onClick.AddListener(() =>
            {
				this.Hide();
            });
        }

		private void ChangeBtnImg(Button btn, bool condition)
		{
			if(condition)
			{
                ResUtil.LoadTextureAsync("T_12_ok_", (tex) =>
				{
					Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
					btn.GetComponent<Image>().sprite = sprite;
				});
			}
			else
			{
				ResUtil.LoadTextureAsync("T_0_empty_", (tex) =>
				{
                    Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                    btn.GetComponent<Image>().sprite = sprite;
                });
            }
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
