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
            Debug.Log($"ÿ��ɱ{EnemyCnt}�����˻ظ�һ������");
        }


        protected override void DoEffectAfterStart()
        {

        }
    }
}