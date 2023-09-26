namespace QFramework.Car
{
    public class ShowLevelUpUICommand : AbstractCommand
    {

        protected override void OnExecute()
        {
            //游戏暂停
            GameArch.Interface.GetSystem<GameSystem>().GamePause();

            var PlayerPower = this.GetModel<PlayerModel>().PlayerPower;
            var commonQueue = PlayerPower.RandomChoosePower(PlayerPower.CommonPowerData);
            var specialQueue = PlayerPower.RandomChoosePower(PlayerPower.SpecialPowerData);

            //通知UI显示选项
            UIKitExtension.OpenPanelAsync<ChoosePowerUI>(uiData : new ChoosePowerUIData
            {
                CommonPowerQueue = commonQueue,
                SpecialPowerQueue = specialQueue
            });

            //在 WebGL 平台上, AssetBundle 加载资源只支持异步加载，所以为此提供了 UIKit 的异步加载支持。
            //StartCoroutine(UIKit.OpenPanelAsync<UIHomePanel>());
            //// 或者
            //UIKit.OpenPanelAsync<UIHomePanel>().ToAction().Start(this);
        }

    }
}