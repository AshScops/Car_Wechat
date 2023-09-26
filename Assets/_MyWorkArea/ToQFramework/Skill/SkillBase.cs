
using System;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public abstract class SkillBase
    {
        protected GameModel GameModel => GameArch.Interface.GetModel<GameModel>();
        protected PlayerModel PlayerModel => GameArch.Interface.GetModel<PlayerModel>();
        protected EnemyModel EnemyModel => GameArch.Interface.GetModel<EnemyModel>();
        protected WeaponModel WeaponModel => GameArch.Interface.GetModel<WeaponModel>();

        public void EffectsBeforeStart()
        {
            DoEffectBeforeStart();
        }

        public void EffectsAfterStart()
        {
            DoEffectAfterStart();
        }

        /// <summary>
        /// ԭ�Ӳ���
        /// </summary>
        protected abstract void DoEffectBeforeStart();

        /// <summary>
        /// ԭ�Ӳ���
        /// </summary>
        protected abstract void DoEffectAfterStart();
    }
}