using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// ����ʱ����Ч������������������˷�
    /// </summary>
    public class ValueCalculateCenter
    {
        private static GameModel gameModel = GameArch.Interface.GetModel<GameModel>();
        private static PlayerModel playerModel = GameArch.Interface.GetModel<PlayerModel>();

        /// <summary>
        /// ��ʽ��atk * skillAtkRate.Value
        /// </summary>
        /// <param name="atk"></param>
        /// <returns>�����Ĺ�����</returns>
        public static float GetAtk(float atk, Type weaponType)
        {
            //Debug.Log(weaponType.Equals(typeof(Pistol)));
            float res = atk * gameModel.SkillAtkRate.Value + gameModel.FixedAtk[weaponType];
            return res < 1 ? 1 : res;
        }

        /// <summary>
        /// ��ʽ��scale * skillAmmoScaleRate.Value
        /// </summary>
        /// <param name="scale"></param>
        /// <returns>�������ӵ����</returns>
        public static Vector3 GetAmmoScale(Vector3 scale)
        {
            return scale * gameModel.SkillAmmoScaleRate.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ammoSpeed"></param>
        /// <param name="weaponType"></param>
        /// <returns>�����ĳ��ٶ�</returns>
        public static float GetAmmoSpeed(float ammoSpeed, Type weaponType)
        {
            return ammoSpeed + gameModel.FixedAmmoSpeed[weaponType];
        }


        /// <summary>
        /// ��ʽ��dmg * vulnerableRate.Value
        /// </summary>
        /// <param name="dmg"></param>
        /// <returns>�������˺�</returns>
        public static float GetDmg(float dmg)
        {
            float res = dmg * gameModel.VulnerableRate.Value;
            return res < 1 ? 1 : res;
        }

        /// <summary>
        /// ��ʽ��pierce * FixedPierce
        /// </summary>
        /// <param name="pierce"></param>
        /// <returns>�����Ĵ�͸��</returns>
        public static int GetPierce(int pierce, Type weaponType)
        {
            int res = pierce + gameModel.FixedPierce[weaponType];
            return res <= 1 ? 1 : res;
        }

        /// <summary>
        /// ��ʽ��scatteringAngle * FixedScatterAngle
        /// </summary>
        /// <param name="scatteringAngle"></param>
        /// <param name="weaponType"></param>
        /// <returns>������ɢ��Ƕ�</returns>
        public static int GetScatteringAngle(int scatteringAngle, Type weaponType)
        {
            return scatteringAngle + gameModel.FixedScatterAngle[weaponType];
        }

        /// <summary>
        /// ��ʽ��reloadCd / skillReloadRate.Value
        /// </summary>
        /// <param name="reloadCd"></param>
        /// <returns>������װ��ʱ��</returns>
        public static float GetReloadCd(float reloadCd, Type weaponType)
        {
            float res = reloadCd / gameModel.SkillReloadRate.Value + gameModel.FixedReloadInterval[weaponType];
            return res <= 0 ? 0.02f : res;
        }

        /// <summary>
        /// ��ʽ��(int)Mathf.Ceil(capacity * skillCapacityRate.Value)
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns>����������</returns>
        public static int GetCapacity(int capacity, Type weaponType)
        {
            int res = (int)Mathf.Ceil(capacity * gameModel.SkillCapacityRate.Value) + gameModel.FixedCapacity[weaponType];
            return gameModel.SkillCapacityOnlyOne.Value ? 1 : res;
        }


        /// <summary>
        /// ��ʽ��fireCd / skillFireRate.Value
        /// </summary>
        /// <param name="ammoCountPerShoot"></param>
        /// <returns>�����ĵ�����</returns>
        public static int GetAmmoCountPerShoot(int ammoCountPerShoot, Type weaponType)
        {
            int res = ammoCountPerShoot + gameModel.FixedAmmoCountPerShoot[weaponType];
            return res <= 0 ? 1 : res;
        }

        /// <summary>
        /// ��ʽ��fireCd / skillFireRate.Value
        /// </summary>
        /// <param name="fireCd"></param>
        /// <returns>�����Ŀ�����</returns>
        public static float GetFireCd(float fireCd, Type weaponType)
        {
            float res = fireCd / gameModel.SkillFireRate.Value + gameModel.FixedFireInterval[weaponType];
            return res <= 0 ? 0.01f : res;
        }

        /// <summary>
        /// ��ʽ��speed * skillPlayerMoveRate.Value
        /// </summary>
        /// <param name="speed"></param>
        /// <returns>�������������</returns>
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
        /// ÿ����ʮ��������
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
