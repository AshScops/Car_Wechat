using System;
using UnityEngine;

namespace QFramework.Car
{
    public abstract class FireWayBase
    {
        /// <summary>
        /// ��д�˷������Զ��������ʽ
        /// </summary>
        public abstract void Fire(Vector3 shootPos, Vector3 targetPos, GameObject ammoPrefab, int scatteringAngle,
            int ammoCountPerShoot, float atk, int pierce, Vector3 scale, float ammoSpeed, Type weaponType);


    }
}