using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class PoisonElemental : BuffBase, Elementable
    {
        public override int GetBuffId()
        {
            return 1;
        }

        public ElementsEnum GetElement()
        {
            return ElementsEnum.PoisonElemental;
        }

        protected override void SetDurationTime(ref float durationTime)
        {
            durationTime = 5f;
        }

        protected override void SetEffectTime(ref float effectTime)
        {
            effectTime = 1f;
        }

        /// <summary>
        /// 开始就有一层
        /// </summary>
        protected override void OnThisGenerate()
        {
            effectCnt = 1;
        }

        protected override void OnThisOverlay()
        {
            if (effectCnt < effectCntLimit)
                effectCnt++;
        }

        protected override void OnThisEffect()
        {
            IDamageable damageable = null;
            if(gameObject.TryGetComponent(out damageable))
            {
                //TODO:分成Dot伤害接口和直接伤害接口
                damageable.GetDamage(effectCnt);
            }
        }

        protected override void OnThisDestroy()
        {
            //层数清零
            effectCnt = 0;
        }


    }
}