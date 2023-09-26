using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class GetExtraDamageRate : SkillBase
    {
        public float damageRate = 0.5f;
        protected override void DoEffectBeforeStart()
        {
            GameModel.SkillAtkRate.Value += damageRate;
        }

        protected override void DoEffectAfterStart()
        {

        }
    }
}