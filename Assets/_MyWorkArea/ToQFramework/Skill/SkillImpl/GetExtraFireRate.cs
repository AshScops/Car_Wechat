using System;

namespace QFramework.Car
{
    [Serializable]
    public class GetExtraFireRate : SkillBase
    {
        public float fire = 0.2f;

        protected override void DoEffectBeforeStart()
        {
            GameModel.SkillFireRate.Value += fire;
        }

        protected override void DoEffectAfterStart()
        {

        }
    }
}