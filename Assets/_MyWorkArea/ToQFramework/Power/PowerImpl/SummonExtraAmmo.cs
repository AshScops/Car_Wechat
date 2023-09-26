using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// 射击时派生子弹，沿弧形路径移动
    /// </summary>
    public class SummonExtraAmmo : PowerBase
    {
        public override int GetPowerId()
        {
            return 8;
        }

        public override void OnAttach()
        {
            //成功, 失败
            m_summonRateArray = new float[] { this.summonRate, 1 - this.summonRate };
            weaponModel.OnAmmoGenerate.Register(Summon);
        }

        public override void OnUnattach()
        {
            weaponModel.OnAmmoGenerate.UnRegister(Summon);
        }

        public float summonRate = 0.3f;
        private float[] m_summonRateArray;
        public float missileSpeed = 25f;

        private void Summon(GameObject ammoGo,Vector3 startPos, Vector3 targetPos)
        {
            //if (RandomUtil.RandomChoose(m_summonRateArray) != 0) return;
            
            var go = GameObject.Instantiate(ammoGo, startPos, Quaternion.identity);
            go.GetComponent<AmmoBase>().ProjectileMoveWay =
                new ProjectileMoveBezier(go.transform, this.missileSpeed, startPos, targetPos);

            //Debug.Log("oldatk" + ammoGo.GetComponent<AmmoBase>().Atk);
            //Debug.Log("newatk" + go.GetComponent<AmmoBase>().Atk);
        }


    }
}