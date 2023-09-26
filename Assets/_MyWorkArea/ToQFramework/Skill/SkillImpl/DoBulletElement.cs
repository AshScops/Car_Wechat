using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    public class DoBulletElement : SkillBase
    {
        protected override void DoEffectBeforeStart()
        {
            WeaponModel.OnAmmoGenerate.Register(GiveBulletElement);
        }

        ElementalBulletParam[] elementalNames = new ElementalBulletParam[] {
            new ElementalBulletParam(typeof(FireElemental), Color.red),
            new ElementalBulletParam(typeof(IceElemental), Color.blue),
            new ElementalBulletParam(typeof(PoisonElemental), Color.green)
        };

        private void GiveBulletElement(GameObject ammoGo, Vector3 startPos, Vector3 targetPos)
        {
            AmmoBase ammo;
            if (!ammoGo.TryGetComponent(out ammo)) return;

            var shouldGiveResult = RandomUtility.Choose(0, 1);
            if (shouldGiveResult == 1) return;

            var elementIndex = RandomUtility.Choose(0, 1, 2);
            ammo.BuffTypesOnHit.Add(elementalNames[elementIndex].Type);

            //¸Ä×Óµ¯¹ì¼£ÑÕÉ«
            var fx = ammoGo.transform.Find("ammoFX Variant");
            if (fx == null) return;

            ParticleSystem ps = fx.GetComponent<ParticleSystem>();
            ParticleSystem.TrailModule tm = ps.trails;
            tm.colorOverTrail = elementalNames[elementIndex].Color;
        }

        protected override void DoEffectAfterStart()
        {

        }
    }
}