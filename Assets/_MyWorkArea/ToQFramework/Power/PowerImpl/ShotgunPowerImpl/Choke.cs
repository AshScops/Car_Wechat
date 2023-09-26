using System;

namespace QFramework.Car
{
    /// <summary>
    /// 霰弹枪：弹丸不再覆盖扇形区域，而是改为并排出膛，并提高弹丸初速度
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