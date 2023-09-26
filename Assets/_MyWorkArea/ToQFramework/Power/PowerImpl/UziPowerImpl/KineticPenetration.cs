using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// ³å·æÇ¹£º¹á´©Ôö¼Ó2£¬³õËÙ¶ÈÌáÉý£¬µ«µ¯¼ÐÈÝÁ¿ÂÔÎ¢½µµÍ
    /// </summary>
    public class KineticPenetration : PowerBase
    {
        public override int GetPowerId()
        {
            return 104;
        }

        private int pierce = 2;
        private float ammoSpeed = 5f;
        private Type weaponType = typeof(Uzi);

        public override void OnAttach()
        {
            gameModel.FixedPierce[weaponType] += pierce;
            gameModel.FixedAmmoSpeed[weaponType] += ammoSpeed;
        }

        public override void OnUnattach()
        {
            gameModel.FixedPierce[weaponType] -= pierce;
            gameModel.FixedAmmoSpeed[weaponType] -= ammoSpeed;

        }
    }
}