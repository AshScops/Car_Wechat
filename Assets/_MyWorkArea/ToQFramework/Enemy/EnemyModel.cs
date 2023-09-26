using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class EnemyModel : AbstractModel
    {
        /// <summary>
        /// 第一个参数为敌人位置
        /// </summary>
        public EasyEvent<Vector3> OnEnemyDead;

        /// <summary>
        /// 第一个参数为敌人位置
        /// </summary>
        public EasyEvent<Vector3> OnEnemyBeHit;


        protected override void OnInit()
        {
            OnEnemyDead = new EasyEvent<Vector3>();
            OnEnemyBeHit = new EasyEvent<Vector3>();
        }
    }
}