using UnityEngine;

namespace QFramework.Car
{
    public class DoHpDown : SkillBase
    {
        protected override void DoEffectBeforeStart()
        {

        }

        protected override void DoEffectAfterStart()
        {
            int HpMax = PlayerModel.MaxHp;
            while (PlayerModel.Hp > HpMax / 2f)
                PlayerModel.Hp.Value--;
        }
    }
}