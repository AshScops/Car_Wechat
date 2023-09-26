using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public abstract class ProjectileMoveBase
    {
        protected Transform objectTrans;
        protected float speed;
        protected Vector3 direction;

        protected ProjectileMoveBase(Transform trans, float speed)
        {
            this.objectTrans = trans;
            this.speed = speed;
        }


        /// <summary>
        /// ��FixedUpdate�е���
        /// </summary>
        public abstract void Move();


    }

}

