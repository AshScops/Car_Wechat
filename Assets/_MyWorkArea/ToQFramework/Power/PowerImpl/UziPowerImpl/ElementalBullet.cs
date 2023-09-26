using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// ���ǹ��Ϊ�ӵ����ʩ��Ԫ�أ�����ħ��Ҫʱ�䣬װ��Ч���½�
    /// </summary>
    public class ElementalBullet : PowerBase
    {
        public override int GetPowerId()
        {
            return 107;
        }

        public override void OnAttach()
        {
            weaponModel.OnAmmoGenerate.Register(BuffIt);
        }

        public override void OnUnattach()
        {
            weaponModel.OnAmmoGenerate.UnRegister(BuffIt);
        }

        ElementalBulletParam[] elementalNames = new ElementalBulletParam[] {
            new ElementalBulletParam(typeof(FireElemental), Color.red),
            new ElementalBulletParam(typeof(IceElemental), Color.blue),
            new ElementalBulletParam(typeof(PoisonElemental), Color.green)
        };

        private void BuffIt(GameObject ammoGo, Vector3 startPos, Vector3 targetPos)
        {
            AmmoBase ammo;
            if (!ammoGo.TryGetComponent<AmmoBase>(out ammo)) return;
            if (ammo.WeaponType != typeof(Uzi)) return;

            var result = RandomUtility.Choose(0, 1, 2);
            //����BuffsOnHit
            ammo.BuffTypesOnHit.Add(elementalNames[result].Type);

            //���ӵ��켣��ɫ
            ParticleSystem ps = ammoGo.transform.Find("ammoFX Variant").GetComponent<ParticleSystem>();
            //ParticleSystem.MainModule main = ps.main;
            //main.startColor = elementalNames[result].Color;
            ParticleSystem.TrailModule tm = ps.trails;
            tm.colorOverTrail = elementalNames[result].Color;
        }
    }
}

public struct ElementalBulletParam
{
    public Type Type;
    public Color Color;

    public ElementalBulletParam(Type t, Color c)
    {
        Type = t;
        Color = c;
    }
}