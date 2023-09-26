using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class EnemyModel : AbstractModel
    {
        /// <summary>
        /// ��һ������Ϊ����λ��
        /// </summary>
        public EasyEvent<Vector3> OnEnemyDead;

        /// <summary>
        /// ��һ������Ϊ����λ��
        /// </summary>
        public EasyEvent<Vector3> OnEnemyBeHit;


        protected override void OnInit()
        {
            OnEnemyDead = new EasyEvent<Vector3>();
            OnEnemyBeHit = new EasyEvent<Vector3>();
        }
    }
}