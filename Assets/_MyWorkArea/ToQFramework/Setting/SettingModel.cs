namespace QFramework.Car
{
    public class SettingModel : AbstractModel
    {
        public BindableProperty<bool> DmgNumEnable;
        public BindableProperty<bool> AudioEnable;
        
        protected override void OnInit()
        {
            DmgNumEnable = new BindableProperty<bool>(true);
            AudioEnable = new BindableProperty<bool>(true);

        }

    }

}
