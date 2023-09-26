using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class IceElemental : BuffBase, Elementable
    {
        public override int GetBuffId()
        {
            return 3;
        }

        public ElementsEnum GetElement()
        {
            return ElementsEnum.IceElemental;
        }

        protected override void SetDurationTime(ref float durationTime)
        {
            durationTime = 5f;
        }

        protected override void SetEffectTime(ref float effectTime)
        {
            effectTime = 1f;
        }

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
            if (gameObject.TryGetComponent(out damageable))
            {
                //TODO:�ֳ�Dot�˺��ӿں�ֱ���˺��ӿ�
                damageable.GetDamage(effectCnt);
            }
        }

        protected override void OnThisDestroy()
        {
            //��������
            effectCnt = 0;
        }
    }
}