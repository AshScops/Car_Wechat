using System;

namespace QFramework.Car
{
    [Serializable]
    public class GetExtraHp : SkillBase
    {
        public int extraHp;

        protected override void DoEffectBeforeStart()
        {
            PlayerModel.MaxHp.Value += extraHp;
        }

        protected override void DoEffectAfterStart()
        {

        }
    }
}