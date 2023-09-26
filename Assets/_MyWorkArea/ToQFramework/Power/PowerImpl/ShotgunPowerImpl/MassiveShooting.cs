using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.Car
{
    /// <summary>
    /// 霰弹枪：显著提升弹丸数和散射角度，但射速显著下降
    /// </summary>
    public class MassiveShooting : PowerBase
    {
        public override int GetPowerId()
        {
            return 108;
        }


        private Type weaponType = typeof(Shotgun);
        private int ammoCountPerShoot = 10;
        private int scatterAngle = 90;
        private float fireInterval = 1f;
        public override void OnAttach()
        {
            gameModel.FixedAmmoCountPerShoot[weaponType] += ammoCountPerShoot;
            gameModel.FixedScatterAngle[weaponType] += scatterAngle;
            gameModel.FixedFireInterval[weaponType] += fireInterval;
        }

        public override void OnUnattach()
        {
            gameModel.FixedAmmoCountPerShoot[weaponType] -= ammoCountPerShoot;
            gameModel.FixedScatterAngle[weaponType] -= scatterAngle;
            gameModel.FixedFireInterval[weaponType] -= fireInterval;
        }

    }
}