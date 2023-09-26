using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public class RecoverHp : SkillBase
    {
        public int EnemyCnt;

        protected override void DoEffectBeforeStart()
        {
            Debug.Log($"每击杀{EnemyCnt}个敌人回复一点生命");
        }


        protected override void DoEffectAfterStart()
        {

        }
    }
}