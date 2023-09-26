using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace QFramework.Car
{
	public partial class FloatingTextCanvas : ViewController
	{
		private static FloatingTextCanvas m_default;

		private void Awake()
		{
			m_default = this;
        }

		private void OnDestroy()
		{
			m_default = null;
		}

		void Start()
		{
            FloatingText.Find("Text").GetComponent<Text>().DOFade(0, 0f);
            FloatingText.Hide();
		}


		public static void ShowFloatingText(bool dmgTextEnabled, Vector3 enemyPos, string text)
		{
            if (!dmgTextEnabled) return;

            m_default.FloatingText.InstantiateWithParent(m_default.transform)
            
            .Self(go =>
            {
                var rect = go.GetComponent<RectTransform>();
                //Overlay
                Vector2 screenPos = Camera.main.WorldToScreenPoint(enemyPos);
                //TODO:���ƫ��
                float offsetEdge = 50f;
                var xoffset = Random.Range(-offsetEdge, offsetEdge);
                var yoffset = Random.Range(-offsetEdge, offsetEdge);
                rect.position = screenPos + Vector2.up * 5 + new Vector2(xoffset, yoffset);
                //Camera
                //Vector2 screenPos = Camera.main.WorldToScreenPoint(enemyPos);
                //Vector2 point;
                //RectTransformUtility.ScreenPointToLocalPointInRectangle(
                //   rect, screenPos, Camera.main, out point);
                //rect.localPosition = point;

                var textTrans = go.Find("Text");
                var textComp = textTrans.GetComponent<Text>();
                textComp.text = text;

                var duration = 0.2f;
                var keep = 0.4f;
                ActionKit.Sequence()
                .Delay(0, () =>
                {
                    //����
                    textComp.DOFade(1, duration);
                })
                .Delay(duration + keep, () =>
                {
                    textComp.DOFade(0, duration);
                })
                .Delay(duration, () =>
                {
                    go.gameObject.DestroySelfGracefully();
                })
                .Start(textComp);
            })
            .Show()
            ;

        }

        private static Vector2 World2DToUgui(Camera cam, Vector3 worldPos, RectTransform rect)
        {
            Vector2 screenPoint = cam.WorldToScreenPoint(worldPos);//��������ת��Ϊ��Ļ����
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            screenPoint -= screenSize / 2;
            //��ʽ�������������screenSize�ȱ���С�����ƹ�һ����������������Ļ����Ϊ��׼��
            Vector2 anchorPos = screenPoint / screenSize * rect.sizeDelta;//���ŵõ�UGUI����
            return anchorPos;
        }
    }
}
