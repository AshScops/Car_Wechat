using System;
using UnityEngine;

namespace QFramework.Car
{
    public class CommonFireWay : FireWayBase
    {
        public override void Fire(Vector3 shootPos, Vector3 targetPos, GameObject ammoPrefab, int scatteringAngle,
            int ammoCountPerShoot, float atk, int pierce, Vector3 scale, float ammoSpeed, Type weaponType)
        {
            Vector3 direction = targetPos - shootPos;
            direction.y = 0;

            int startAngle = -scatteringAngle / 2;
            int endAngle = scatteringAngle / 2;
            for (int i = 0; i < ammoCountPerShoot; i++)
            {
                float angle = UnityEngine.Random.Range(startAngle, endAngle);
                Vector3 newDirection = Quaternion.Euler(0, angle, 0) * direction;
                newDirection.y = 0;
                //print(angle);

                GameObject go = GameObject.Instantiate(ammoPrefab, shootPos, Quaternion.identity);
                go.transform.rotation = Quaternion.LookRotation(newDirection);
                go.transform.rotation = Quaternion.Euler(0, go.transform.eulerAngles.y, 0);
                go.transform.localScale = scale;

                AmmoBase ammo = go.GetComponent<AmmoBase>();
                ammo.ProjectileMoveWay = new ProjectileMoveStriaght(go.transform, ammoSpeed, newDirection);
                ammo.Atk = atk;
                ammo.Pierce = pierce;
                ammo.WeaponType = weaponType;

                //投射物生成时触发
                GameArch.Interface.GetModel<WeaponModel>().OnAmmoGenerate.Trigger(go, shootPos, targetPos);
            }
            
        }
    }
}