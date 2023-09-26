using System;
using UnityEngine;

namespace QFramework.Car
{
    public class SalvoFireWay : FireWayBase
    {
        public override void Fire(Vector3 shootPos, Vector3 targetPos, GameObject ammoPrefab, int scatteringAngle,
            int ammoCountPerShoot, float atk, int pierce, Vector3 scale, float ammoSpeed, Type weaponType)
        {
            if (ammoPrefab == null) return;

            Vector3 direction = targetPos - shootPos;
            direction.y = 0;
            direction.Normalize();

            Vector3 horizontalDirection = (Quaternion.Euler(0, 90, 0) * direction).normalized;
            //float distanceBetweenAmmo = 0.25f * scatteringAngle / 30f;
            float distanceBetweenAmmo = 0.3f;
            Vector3 startPos;
            if (ammoCountPerShoot % 2 == 0)
            {
                //��ʼ���ɵ� = ����� + ����һ������ + ����(ammoCountPerShoot / 2 - 1)��ȫ���
                startPos = shootPos + horizontalDirection * distanceBetweenAmmo / 2f + horizontalDirection * (ammoCountPerShoot / 2 - 1) * distanceBetweenAmmo;
            }
            else
            {
                //��ʼ���ɵ� = ����� + ����((ammoCountPerShoot - 1) / 2)��ȫ���
                startPos = shootPos + horizontalDirection * ((ammoCountPerShoot - 1) / 2) * distanceBetweenAmmo;
            }

            for (int i = 0; i < ammoCountPerShoot; i++)
            {
                GameObject go = GameObject.Instantiate(ammoPrefab, 
                    startPos - horizontalDirection * i * distanceBetweenAmmo, Quaternion.identity);
                go.transform.rotation = Quaternion.LookRotation(direction);
                go.transform.rotation = Quaternion.Euler(0, go.transform.eulerAngles.y, 0);
                go.transform.localScale = scale;

                AmmoBase ammo = go.GetComponent<AmmoBase>();
                ammo.ProjectileMoveWay = new ProjectileMoveStriaght(go.transform, ammoSpeed, direction);
                ammo.Atk = atk;
                ammo.Pierce = pierce;
                ammo.WeaponType = weaponType;

                //Ͷ��������ʱ����
                GameArch.Interface.GetModel<WeaponModel>().OnAmmoGenerate.Trigger(go, shootPos, targetPos);
            }
        }
    }
}