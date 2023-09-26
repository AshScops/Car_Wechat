
using UnityEngine;

namespace QFramework.Car
{
    public class RocketAmmo : AmmoBase
    {
        public float RocketExplosionRadius = 3f;

        protected override void Start()
        {
            lifeCdTimer = new CooldownTimer(this.LifeCd);
        }

        public float rocketExplosionRadius = 3f;
        public GameObject rocketExplosionFX;

        protected override void HandleHitEnemy(Collider other)
        {
            float r = ValueCalculateCenter.GetExplosionRadius(this.rocketExplosionRadius);
            var colliders = Physics.OverlapSphere(other.ClosestPoint(this.transform.position), r);
            foreach (var target in colliders)
            {
                if (target.CompareTag("Enemy"))
                {
                    IDamageable damageable;
                    target.transform.TryGetComponent(out damageable);
                    if (damageable != null)
                    {
                        damageable.GetDamage(this.Atk);
                    }
                }
            }

            ResUtil.GenerateGOAsync("Rocket Explosion", other.ClosestPoint(this.transform.position), (go) =>
            {
                go.transform.localScale *= r / rocketExplosionRadius;
            });
            
        }


    }
}