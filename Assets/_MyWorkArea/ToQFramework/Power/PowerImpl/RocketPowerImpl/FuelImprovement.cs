using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// »ð¼ýÍ²£º¸ü´óµÄ±¬Õ¨·¶Î§£¬µ«³õËÙ¶ÈÂÔÎ¢½µµÍ
    /// </summary>
    public class FuelImprovement : PowerBase
    {
        public override int GetPowerId()
        {
            return 105;
        }

        private float explosionRadius = 3f;
        private float ammoSpeed = -5f;
        private Type weaponType = typeof(RocketLauncher);
        public override void OnAttach()
        {
            gameModel.FixedExplosionRadius.Value += explosionRadius;
            gameModel.FixedAmmoSpeed[weaponType] += ammoSpeed;
        }

        public override void OnUnattach()
        {
            gameModel.FixedExplosionRadius.Value -= explosionRadius;
            gameModel.FixedAmmoSpeed[weaponType] -= ammoSpeed;
        }

    }
}