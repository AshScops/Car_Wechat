using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.Car
{
    public class WeaponModel : AbstractModel
    {
        public EasyEvent OnWeaponExecute = new EasyEvent();

        /// <summary>
        /// ��һ��Vec3Ϊ�ӵ���λ�ã��ڶ���Vec3ΪĿ��λ��(pos)
        /// </summary>
        public EasyEvent<GameObject, Vector3, Vector3> OnAmmoGenerate = new EasyEvent<GameObject, Vector3, Vector3>();

        public EasyEvent OnWeaponReload = new EasyEvent();

        /// <summary>
        /// Vec3Ϊ��ײλ��
        /// </summary>
        public EasyEvent<GameObject, Vector3> OnAmmoHit = new EasyEvent<GameObject, Vector3>();

        public EasyEvent<FireWayBase, Type> ChangeWeaponFireWay = new EasyEvent<FireWayBase, Type>();



        protected override void OnInit()
        {
            GameArch.Interface.GetModel<GameModel>().ResetAllValue.Register(() =>
            {
                OnWeaponExecute = new EasyEvent();
                OnAmmoGenerate = new EasyEvent<GameObject, Vector3, Vector3>();
                OnWeaponReload = new EasyEvent();
                OnAmmoHit = new EasyEvent<GameObject, Vector3>();
                ChangeWeaponFireWay = new EasyEvent<FireWayBase, Type>();
            });
        }

      
    }
}