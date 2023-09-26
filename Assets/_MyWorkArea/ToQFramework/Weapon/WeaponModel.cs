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
        /// 第一个Vec3为子弹初位置，第二个Vec3为目标位置(pos)
        /// </summary>
        public EasyEvent<GameObject, Vector3, Vector3> OnAmmoGenerate = new EasyEvent<GameObject, Vector3, Vector3>();

        public EasyEvent OnWeaponReload = new EasyEvent();

        /// <summary>
        /// Vec3为碰撞位置
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