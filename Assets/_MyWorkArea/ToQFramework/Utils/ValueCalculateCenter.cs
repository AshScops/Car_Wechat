using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// 所有时间类效果按除法，其余均按乘法
    /// </summary>
    public class ValueCalculateCenter
    {
        private static GameModel gameModel = GameArch.Interface.GetModel<GameModel>();
        private static PlayerModel playerModel = GameArch.Interface.GetModel<PlayerModel>();

        /// <summary>
        /// 公式：atk * skillAtkRate.Value
        /// </summary>
        /// <param name="atk"></param>
        /// <returns>计算后的攻击力</returns>
        public static float GetAtk(float atk, Type weaponType)
        {
            //Debug.Log(weaponType.Equals(typeof(Pistol)));
            float res = atk * gameModel.SkillAtkRate.Value + gameModel.FixedAtk[weaponType];
            return res < 1 ? 1 : res;
        }

        /// <summary>
        /// 公式：scale * skillAmmoScaleRate.Value
        /// </summary>
        /// <param name="scale"></param>
        /// <returns>计算后的子弹体积</returns>
        public static Vector3 GetAmmoScale(Vector3 scale)
        {
            return scale * gameModel.SkillAmmoScaleRate.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ammoSpeed"></param>
        /// <param name="weaponType"></param>
        /// <returns>计算后的初速度</returns>
        public static float GetAmmoSpeed(float ammoSpeed, Type weaponType)
        {
            return ammoSpeed + gameModel.FixedAmmoSpeed[weaponType];
        }


        /// <summary>
        /// 公式：dmg * vulnerableRate.Value
        /// </summary>
        /// <param name="dmg"></param>
        /// <returns>计算后的伤害</returns>
        public static float GetDmg(float dmg)
        {
            float res = dmg * gameModel.VulnerableRate.Value;
            return res < 1 ? 1 : res;
        }

        /// <summary>
        /// 公式：pierce * FixedPierce
        /// </summary>
        /// <param name="pierce"></param>
        /// <returns>计算后的穿透数</returns>
        public static int GetPierce(int pierce, Type weaponType)
        {
            int res = pierce + gameModel.FixedPierce[weaponType];
            return res <= 1 ? 1 : res;
        }

        /// <summary>
        /// 公式：scatteringAngle * FixedScatterAngle
        /// </summary>
        /// <param name="scatteringAngle"></param>
        /// <param name="weaponType"></param>
        /// <returns>计算后的散射角度</returns>
        public static int GetScatteringAngle(int scatteringAngle, Type weaponType)
        {
            return scatteringAngle + gameModel.FixedScatterAngle[weaponType];
        }

        /// <summary>
        /// 公式：reloadCd / skillReloadRate.Value
        /// </summary>
        /// <param name="reloadCd"></param>
        /// <returns>计算后的装弹时间</returns>
        public static float GetReloadCd(float reloadCd, Type weaponType)
        {
            float res = reloadCd / gameModel.SkillReloadRate.Value + gameModel.FixedReloadInterval[weaponType];
            return res <= 0 ? 0.02f : res;
        }

        /// <summary>
        /// 公式：(int)Mathf.Ceil(capacity * skillCapacityRate.Value)
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns>计算后的容量</returns>
        public static int GetCapacity(int capacity, Type weaponType)
        {
            int res = (int)Mathf.Ceil(capacity * gameModel.SkillCapacityRate.Value) + gameModel.FixedCapacity[weaponType];
            return gameModel.SkillCapacityOnlyOne.Value ? 1 : res;
        }


        /// <summary>
        /// 公式：fireCd / skillFireRate.Value
        /// </summary>
        /// <param name="ammoCountPerShoot"></param>
        /// <returns>计算后的弹丸数</returns>
        public static int GetAmmoCountPerShoot(int ammoCountPerShoot, Type weaponType)
        {
            int res = ammoCountPerShoot + gameModel.FixedAmmoCountPerShoot[weaponType];
            return res <= 0 ? 1 : res;
        }

        /// <summary>
        /// 公式：fireCd / skillFireRate.Value
        /// </summary>
        /// <param name="fireCd"></param>
        /// <returns>计算后的开火间隔</returns>
        public static float GetFireCd(float fireCd, Type weaponType)
        {
            float res = fireCd / gameModel.SkillFireRate.Value + gameModel.FixedFireInterval[weaponType];
            return res <= 0 ? 0.01f : res;
        }

        /// <summary>
        /// 公式：speed * skillPlayerMoveRate.Value
        /// </summary>
        /// <param name="speed"></param>
        /// <returns>计算后的玩家移速</returns>
        public static float GetPlayerSpeed(float speed)
        {
            return speed * gameModel.SkillPlayerMoveRate.Value;
        }

        public static float GetExplosionRadius(float rocketExplosionRadius)
        {
            return rocketExplosionRadius + gameModel.FixedExplosionRadius;
        }

        public static float GetSpawnInterval()
        {
            var res = gameModel.SpawnInterval - playerModel.CurrentLevel * 0.2f;
            return res <= 0.001f ? 0.001f : res;
        }

        /// <summary>
        /// 每级加十敌人上限
        /// </summary>
        /// <returns></returns>
        public static int GetEnemyCountLimit()
        {
            return gameModel.EnemyCountLimit + 10 * playerModel.CurrentLevel;
        }

        public static float GetEnemySpeed(float moveSpeed)
        {
            return moveSpeed + 0.2f * playerModel.CurrentLevel;
        }
    }
}
