using System;

namespace QFramework.Car
{
    /// <summary>
    /// ����ǹ�����費�ٸ����������򣬶��Ǹ�Ϊ���ų��ţ�����ߵ�����ٶ�
    /// </summary>
    public class Choke : PowerBase
    {
        public override int GetPowerId()
        {
            return 103;
        }


        private float ammoSpeed = 5f;
        private Type weaponType = typeof(Shotgun);
        public override void OnAttach()
        {
            weaponModel.ChangeWeaponFireWay.Trigger(new SalvoFireWay(), weaponType);
            gameModel.FixedAmmoSpeed[weaponType] += ammoSpeed;
        }

        public override void OnUnattach()
        {
            weaponModel.ChangeWeaponFireWay.Trigger(new CommonFireWay(), weaponType);
            gameModel.FixedAmmoSpeed[weaponType] -= ammoSpeed;
        }
    }
}