namespace QFramework.Car
{
    public static class UIKitExtension
    {
        /// <summary>
        /// ��չ�������������첽��Panel��Э��ע�ᵽGameController��
        /// </summary>
        /// <returns></returns>
        public static void OpenPanelAsync<T>(UILevel canvasLevel = UILevel.Common, IUIData uiData = null) where T : UIPanel
        {
            GameController.Instance.StartCoroutine(UIKit.OpenPanelAsync<T>(canvasLevel, uiData));
        }
    }
}