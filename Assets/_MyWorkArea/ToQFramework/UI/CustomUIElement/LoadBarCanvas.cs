namespace QFramework.Car
{
	public partial class LoadBarCanvas : ViewController
	{
        private static LoadBarCanvas m_default;

        private void Awake()
        {
            m_default = this;
            m_default.Show();
        }
        private void OnDestroy()
        {
            m_default = null;
        }

        public static void ShowLoadProgress(float progress, string ABName)
        {
            m_default.LoadBar.value = progress;
            m_default.LoadText.text = $"正在加载{ABName}资源中...大约3-8秒...";

            if(progress == 1f)
            {
                m_default.Hide();
            }
            else
            {
                m_default.Show();
            }
        }

        public static void Hide()
        {
            m_default.Hide();
        }
    }
}
