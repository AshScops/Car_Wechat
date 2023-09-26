using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// 换弹时向周围释放冲击波
    /// </summary>
    public class SummonShockWave : PowerBase
    {
        public override int GetPowerId()
        {
            return 9;
        }

        public override void OnAttach()
        {
            //成功，失败
            m_summonRateArray = new float[] { this.summonRate, 1 - this.summonRate };
            m_playerTrans = GameArch.Interface.GetModel<PlayerModel>().PlayerTrans;
            weaponModel.OnWeaponReload.Register(Summon);
        }

        public override void OnUnattach()
        {
            weaponModel.OnWeaponReload.Register(Summon);
        }

        public float summonRate = 0.3f;
        private float[] m_summonRateArray;
        private Transform m_playerTrans;
        public float radius = 5f;
        public float atk = 5f;
        public string shockWaveFxName = "Shock Wave";
        public string hitFxName = "Ammo Hit";

        private void Summon()
        {
            //if (RandomUtil.RandomChoose(m_summonRateArray) != 0) return;

            ResUtil.GenerateGOAsync(shockWaveFxName, m_playerTrans.position, (go) =>
            {
                go.transform.localScale *= 3f;

                AoeUtil.AoeEffect(m_playerTrans.position, radius, dmgCallback: (damageable, hitPos) =>
                {
                    ResUtil.GenerateGOAsync(hitFxName, hitPos, (go) =>
                    {
                        damageable.GetDamage(atk);
                    });
                });
            });

        }

    }
}