using UnityEngine;
using UnityEngine.Events;

namespace QFramework.Car
{
    public static class AoeUtil
    {
        public static void AoeEffect(Vector3 targetPos, float radius, 
            UnityAction<IDamageable, Vector3> dmgCallback = null,
            UnityAction<BuffHandleable, Vector3> buffCallback = null)
        {
            var colliders = Physics.OverlapSphere(targetPos, radius);
            foreach (var collider in colliders)
            {
                if (!collider.CompareTag("Enemy")) continue;

                IDamageable damageable = null;
                collider.TryGetComponent<IDamageable>(out damageable);
                if (damageable != null && dmgCallback != null)
                {
                    dmgCallback(damageable, collider.ClosestPoint(targetPos));
                    continue;
                }

                BuffHandleable buffHandleable = null;
                collider.TryGetComponent<BuffHandleable>(out buffHandleable);
                if (buffHandleable != null && buffCallback != null)
                {
                    buffCallback(buffHandleable, collider.ClosestPoint(targetPos));
                    continue;
                }

            }
        }
    }
}