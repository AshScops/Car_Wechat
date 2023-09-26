namespace QFramework.Car
{
    public class ShowLevelUpUICommand : AbstractCommand
    {

        protected override void OnExecute()
        {
            //��Ϸ��ͣ
            GameArch.Interface.GetSystem<GameSystem>().GamePause();

            var PlayerPower = this.GetModel<PlayerModel>().PlayerPower;
            var commonQueue = PlayerPower.RandomChoosePower(PlayerPower.CommonPowerData);
            var specialQueue = PlayerPower.RandomChoosePower(PlayerPower.SpecialPowerData);

            //֪ͨUI��ʾѡ��
            UIKitExtension.OpenPanelAsync<ChoosePowerUI>(uiData : new ChoosePowerUIData
            {
                CommonPowerQueue = commonQueue,
                SpecialPowerQueue = specialQueue
            });

            //�� WebGL ƽ̨��, AssetBundle ������Դֻ֧���첽���أ�����Ϊ���ṩ�� UIKit ���첽����֧�֡�
            //StartCoroutine(UIKit.OpenPanelAsync<UIHomePanel>());
            //// ����
            //UIKit.OpenPanelAsync<UIHomePanel>().ToAction().Start(this);
        }

    }
}