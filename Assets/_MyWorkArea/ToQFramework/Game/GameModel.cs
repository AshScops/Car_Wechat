using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public enum GameStates
    {
        isRunning = 0,
        isPaused,
        isOver
    }

    public class GameModel : AbstractModel
    {
        public EasyEvent ResetAllValue;
        public BindableProperty<GameStates> GameState = new BindableProperty<GameStates>();
        public BindableProperty<int> Score = new BindableProperty<int>(0); 

        public float SpawnInterval;

        public float SpawnHeight;

        public float SpawnSafeEnemyRadius;
            
        public float SpawnSafePlayerRadius;

        public int EnemyCountLimit;

        public List<Enemy> EnemyList;

        public EasyEvent<int> OnCalcDmg = new EasyEvent<int>();

        public BindableProperty<int> SumDmg = new BindableProperty<int>(0);

        /// <summary>
        /// ��Ϸ��ʼǰһ�̱����ã�GameSystem-GameStart��
        /// </summary>
        public EasyEvent BeforeGameStart = new EasyEvent();

        /// <summary>
        /// ��Ϸ��ʼ�����̱����ã�GameSystem-GameStart��
        /// </summary>
        public EasyEvent AfterGameStart = new EasyEvent();

        /// <summary>
        /// ����PowerӰ��
        /// </summary>
        public BindableProperty<float> SkillAtkRate = new BindableProperty<float>(1.0f);

        public BindableProperty<float> SkillAmmoScaleRate = new BindableProperty<float>(1.0f);

        public BindableProperty<float> SkillPlayerMoveRate = new BindableProperty<float>(1.0f);

        public BindableProperty<float> SkillFireRate = new BindableProperty<float>(1.0f);

        public BindableProperty<float> SkillReloadRate = new BindableProperty<float>(1.0f);

        public BindableProperty<float> SkillCapacityRate = new BindableProperty<float>(1.0f);

        public BindableProperty<bool> SkillCapacityOnlyOne = new BindableProperty<bool>(false);

        public BindableProperty<float> VulnerableRate = new BindableProperty<float>(1.0f);


        //����Fixed���ξ�Ϊ����(�̶���ֵ)����Fixed���μ�����
        /// <summary>
        /// ������ֵ����-����
        /// </summary>
        public Dictionary<Type, float> FixedAtk = new Dictionary<Type, float>();

        /// <summary>
        /// ������ֵ����-������
        /// </summary>
        public Dictionary<Type, int> FixedCapacity = new Dictionary<Type, int>();
        public EasyEvent ResetCapacity = new EasyEvent();

        /// <summary>
        /// ������ֵ����-������
        /// </summary>
        public Dictionary<Type, int> FixedAmmoCountPerShoot = new Dictionary<Type, int>();

        /// <summary>
        /// ������ֵ����-ɢ��
        /// </summary>
        public Dictionary<Type, int> FixedScatterAngle = new Dictionary<Type, int>();

        /// <summary>
        /// ������ֵ����-��͸
        /// </summary>
        public Dictionary<Type, int> FixedPierce = new Dictionary<Type, int>();

        /// <summary>
        /// ������ֵ����-������
        /// </summary>
        public Dictionary<Type, float> FixedFireInterval = new Dictionary<Type, float>();

        /// <summary>
        /// ������ֵ����-װ��ʱ��
        /// </summary>
        public Dictionary<Type, float> FixedReloadInterval = new Dictionary<Type, float>();

        /// <summary>
        /// ������ֵ����-���ٶ�
        /// </summary>
        public Dictionary<Type, float> FixedAmmoSpeed = new Dictionary<Type, float>();

        /// <summary>
        /// ��ը��Χ����
        /// </summary>
        public BindableProperty<float> FixedExplosionRadius = new BindableProperty<float>(0f);
        

        private void InitDic()
        {
            Type[] weaponTypes = new Type[] { typeof(Pistol), typeof(Uzi), typeof(Shotgun), typeof(RocketLauncher) };
            for (int i = 0; i < weaponTypes.Length; i++)
            {
                FixedAtk[weaponTypes[i]] = 0f;
                FixedCapacity[weaponTypes[i]] = 0;
                FixedAmmoCountPerShoot[weaponTypes[i]] = 0;
                FixedScatterAngle[weaponTypes[i]] = 0;
                FixedPierce[weaponTypes[i]] = 0;
                FixedFireInterval[weaponTypes[i]] = 0f;
                FixedReloadInterval[weaponTypes[i]] = 0f;
                FixedAmmoSpeed[weaponTypes[i]] = 0f;
                
            }
        }

        protected override void OnInit()
        {
            ResetAllValue = new EasyEvent();
            Score.Value = 0;
            SumDmg.Value = 0;
            EnemyList = new List<Enemy>();
            GameState.Value = GameStates.isOver;
            SpawnInterval = 1f;
            SpawnHeight = 1f;
            SpawnSafeEnemyRadius = 2f;
            SpawnSafePlayerRadius = 10f;
            EnemyCountLimit = 20;

            InitDic();

            OnCalcDmg.Register((dmg) =>
            {
                SumDmg.Value += dmg;
            });

            ResetAllValue.Register(() =>
            {
                Score.Value = 0;
                SumDmg.Value = 0;
                EnemyList.Clear();
                SpawnInterval = 1f;
                SpawnHeight = 1f;
                SpawnSafeEnemyRadius = 2f;
                SpawnSafePlayerRadius = 10f;
                EnemyCountLimit = 20;

                SkillAtkRate.Value = 1.0f;
                SkillAmmoScaleRate.Value = 1.0f;
                SkillPlayerMoveRate.Value = 1.0f;
                SkillPlayerMoveRate.Value = 1.0f;
                SkillFireRate.Value = 1.0f;
                SkillReloadRate.Value = 1.0f;
                SkillCapacityRate.Value = 1.0f;
                SkillCapacityOnlyOne.Value = false;
                VulnerableRate.Value = 1.0f;

                InitDic();
            });
        }




    }

}

