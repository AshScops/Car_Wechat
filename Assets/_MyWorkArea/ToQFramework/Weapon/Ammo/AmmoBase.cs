using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class AmmoBase : ViewController, IController
    {
        public Type WeaponType;

        public ProjectileMoveBase ProjectileMoveWay;

        private float atk = 0;
        public float Atk { get => atk; set => atk = value; }
        public int Pierce = 1;

        public float LifeCd = 3f;
        protected CooldownTimer lifeCdTimer;

        public List<Type> BuffTypesOnHit = new List<Type>();

        public string HitFxName = "Ammo Hit";


        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        protected virtual void Start()
        {
            lifeCdTimer = new CooldownTimer(this.LifeCd);
        }

        protected void Update()
        {
            if (this.GetModel<GameModel>().GameState != GameStates.isRunning) return;

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            if (lifeCdTimer == null) return;
            if (lifeCdTimer.CoolDownOnUpdate(Time.deltaTime))
            {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (this.GetModel<GameModel>().GameState != GameStates.isRunning) return;

            OnFixedUpdate();
        }

        protected virtual void OnFixedUpdate()
        {
            if (ProjectileMoveWay == null) return;
            ProjectileMoveWay?.Move();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                HandleHitEnemy(other);

                Pierce--;
                if (Pierce > 0) return;

                Destroy(this.gameObject);
            }
        }

        protected virtual void HandleHitEnemy(Collider other)
        {
            IDamageable damageable;
            if (other.transform.TryGetComponent(out damageable))
            {
                damageable.GetDamage(this.atk);
                this.GetModel<WeaponModel>().OnAmmoHit.Trigger(gameObject, other.ClosestPoint(transform.position));

                BuffHandleable buffHandleable = null;
                if (other.gameObject.TryGetComponent<BuffHandleable>(out buffHandleable))
                {
                    for (int i = 0; i < BuffTypesOnHit.Count; i++)
                    {
                        buffHandleable.GetBuffHandler().Add(BuffTypesOnHit[i]);
                    }
                }
                BuffTypesOnHit.Clear();
            }

            ResUtil.GenerateGOAsync(HitFxName, other.ClosestPoint(this.transform.position));
        }


    }

}
