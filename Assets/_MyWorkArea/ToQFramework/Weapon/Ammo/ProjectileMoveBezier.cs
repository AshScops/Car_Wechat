using UnityEngine;


namespace QFramework.Car
{
    public class ProjectileMoveBezier : ProjectileMoveBase
    {
        private int i = 0;
        private int segmentNum = 15;

        private Vector3[] paths;

        private readonly float middlePosDistance = 3f;
        public ProjectileMoveBezier(Transform trans, float speed, Vector3 startPos, Vector3 targetPos) : 
            base(trans, speed)
        {
            Vector3 middlePos = (startPos + targetPos) / 2f;
            Vector3 dir = targetPos - startPos;
            dir = Quaternion.Euler(0, 90, 0) * dir;
            dir.Normalize();
            float randomOffset = Random.Range(-middlePosDistance, middlePosDistance);
            if (randomOffset > 0)
                randomOffset += middlePosDistance;
            else
                randomOffset -= middlePosDistance;
            middlePos += dir * randomOffset;
            paths = BezierUtils.GetBeizerPointList(startPos, middlePos, targetPos, segmentNum);
        }

        public override void Move()
        {
            if(i >= segmentNum)
            {
                objectTrans.position += this.direction * this.speed * Time.deltaTime;
            }
            else
            {
                this.direction = (paths[i] - objectTrans.position);
                direction.y = 0;
                direction.Normalize();
                objectTrans.position = new Vector3(paths[i].x, objectTrans.position.y , paths[i].z);

                Quaternion targetRot = Quaternion.LookRotation(direction);
                objectTrans.rotation = targetRot;

                i++;
            }


        }
    }
}