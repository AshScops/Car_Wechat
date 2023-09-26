using System;

namespace QFramework.Car
{
    [Serializable]
    public class GetExtraReloadRate : SkillBase
    {
        public float reload = 0.2f;

        protected override void DoEffectBeforeStart()
        {
            GameModel.SkillReloadRate.Value += reload;
        }

        protected override void DoEffectAfterStart()
        {

        }
    }
}