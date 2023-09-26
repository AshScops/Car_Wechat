using UnityEngine;

namespace QFramework.Car
{
    public class ProjectileMoveStriaght : ProjectileMoveBase
    {
        public ProjectileMoveStriaght(Transform trans, float speed, Vector3 direction) : base(trans, speed)
        {
            this.direction = direction;
        }

        public override void Move()
        {
            direction = direction.normalized;
            this.objectTrans.position += direction * this.speed * Time.deltaTime;
        }

    }
}
