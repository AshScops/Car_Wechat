using System;
using UnityEngine;

namespace QFramework.Car
{
    public abstract class ShootWeapon : WeaponBase
    {
        public FireWayBase fireWay;

        protected GameObject ammoPrefab = null;
        protected Transform shootTrans;
        protected ParticleSystem fireFX;

        [SerializeField]
        protected int scatteringAngle = 20;

        [SerializeField]
        protected int pierce = 1;

        [SerializeField]
        protected int ammoCountPerShoot = 1;

        protected override void Awake()
        {
            base.Awake();
            InitAmmo();
            this.fireWay = new CommonFireWay();
            this.shootTrans = transform.Find("ShootPos");
            this.fireFX = shootTrans.GetChild(0).GetComponent<ParticleSystem>();

            //注册事件
            this.GetModel<WeaponModel>().ChangeWeaponFireWay.Register(ChangeFireWay).
                UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected abstract void InitAmmo();


        public override bool WeaponExecute()
        {
            if (ammoPrefab == null) return false;

            //增量半径
            float r = 1f;
            while (r <= radius)
            {
                var colliders = Physics.OverlapSphere(this.transform.position, r);
                foreach (var target in colliders)
                {
                    if (target.CompareTag("Enemy"))
                    {
                        //Debug.LogError("发现敌人");

                        fireWay.Fire(
                            shootTrans.position,
                            target.transform.position,
                            ammoPrefab,
                            ValueCalculateCenter.GetScatteringAngle(scatteringAngle, this.GetType()),
                            ValueCalculateCenter.GetAmmoCountPerShoot(ammoCountPerShoot, this.GetType()),
                            ValueCalculateCenter.GetAtk(atk, this.GetType()),
                            ValueCalculateCenter.GetPierce(pierce, this.GetType()),
                            ValueCalculateCenter.GetAmmoScale(Vector3.one),
                            ValueCalculateCenter.GetAmmoSpeed(ammoSpeed, this.GetType()),
                            this.GetType());

                        this.fireCdTimer.ResetCD(ValueCalculateCenter.GetFireCd(this.fireCd, this.GetType()));
                        this.currentCapacity--;
                        AudioKit.PlaySound("single_shot");
                        this.fireFX.Play();

                        return true;
                    }
                }
                r += 1f;
            }

            return false;
        }


        public void ChangeFireWay(FireWayBase fireWay, Type weaponType)
        {
            if (!weaponType.Equals(this.GetType())) return;

            this.fireWay = fireWay;
        }

    }

}
