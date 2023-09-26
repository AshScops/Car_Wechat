using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// �ӵ�������2��
    /// �˺����1.5��
    /// </summary>
    public class BiggerAmmo : PowerBase
    {
        public override int GetPowerId()
        {
            return 1;
        }

        public override void OnAttach()
        {
            gameModel.SkillAmmoScaleRate.Value += 1f;
            gameModel.SkillAtkRate.Value += 0.5f;
        }

        public override void OnUnattach()
        {
            gameModel.SkillAmmoScaleRate.Value -= 1f;
            gameModel.SkillAtkRate.Value -= 0.5f;
        }

    }
}