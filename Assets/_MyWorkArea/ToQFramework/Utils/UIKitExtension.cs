namespace QFramework.Car
{
    public static class UIKitExtension
    {
        /// <summary>
        /// 扩展方法，把所有异步打开Panel的协程注册到GameController上
        /// </summary>
        /// <returns></returns>
        public static void OpenPanelAsync<T>(UILevel canvasLevel = UILevel.Common, IUIData uiData = null) where T : UIPanel
        {
            GameController.Instance.StartCoroutine(UIKit.OpenPanelAsync<T>(canvasLevel, uiData));
        }
    }
}