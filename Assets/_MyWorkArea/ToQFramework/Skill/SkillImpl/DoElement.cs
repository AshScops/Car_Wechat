using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public class DoElement : SkillBase
    {
        [EnumPaging]
        public ElementsEnum element;

        public float radius = 5f;


        public Transform TargetTrans;

        protected override void DoEffectBeforeStart()
        {
            WeaponModel.OnWeaponReload.Register(SetElement);
        }

        private void SetElement()
        {
            AoeUtil.AoeEffect(GameArch.Interface.GetModel<PlayerModel>().PlayerTrans.position,
                radius, null, (buffHandleable, hitPos) =>
            {
                buffHandleable.GetBuffHandler().Add(Type.GetType("QFramework.Car." + element.ToString()), 5);
            });
            
        }

        protected override void DoEffectAfterStart()
        {
        }
    }
}