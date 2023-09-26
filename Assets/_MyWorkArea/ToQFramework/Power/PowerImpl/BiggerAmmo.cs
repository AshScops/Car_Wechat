using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// 子弹体积变大2倍
    /// 伤害提高1.5倍
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