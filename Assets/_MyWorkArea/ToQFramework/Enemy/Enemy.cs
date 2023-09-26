using System.Collections;
using UnityEngine;

namespace QFramework.Car
{
    public enum EnemyStates
    {
        alive = 0,
        dead
    }

	public partial class Enemy : ViewController, IController, IDamageable, BuffHandleable
    {
        protected GameModel m_gameModel;
        protected PlayerModel m_playerModel;
        protected EnemyModel m_enemyModel;

        protected EnemyStates m_enemyState;

        public float MoveSpeed = 3f;
        public float RotSpeed = 10f;

        protected const int RAW_HP = 30;
        [SerializeField]
        protected int m_hp = RAW_HP;

        protected ResLoader resLoader = ResLoader.Allocate();
        public string destoryFxName = "Car Destory";


        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        private BuffHandler m_buffHandler;

        public BuffHandler GetBuffHandler()
        {
            if(m_buffHandler == null)
                m_buffHandler = new BuffHandler(gameObject);
            return m_buffHandler;
        }

        private void Awake()
        {
            m_gameModel = this.GetModel<GameModel>();
            m_playerModel = this.GetModel<PlayerModel>();
            m_enemyModel = this.GetModel<EnemyModel>();
            m_enemyState = EnemyStates.alive;
        }

        void Start()
		{
        }

        protected void Update()
        {
            if (m_gameModel.GameState != GameStates.isRunning) return;

            Move();
        }

        private void Move()
        {
            Vector3 playerPos = m_playerModel.PlayerTrans.position;
            Vector3 direction = playerPos - this.transform.position;

            direction = direction.normalized;
            //位移
            transform.position += direction * ValueCalculateCenter.GetEnemySpeed(MoveSpeed)  * Time.deltaTime;

            //旋转
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                targetRot, RotSpeed * Time.deltaTime);
            //限制只沿Y轴旋转
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }


        public void GetDamage(float dmg)
        {
            if (m_enemyState == EnemyStates.dead) return;

            //后续可能新增各类增减伤乘区，则在伤害结算前一刻计算总伤害
            float sumDmg = ValueCalculateCenter.GetDmg(dmg);

            if (m_hp - sumDmg <= 0)
            {
                m_enemyState = EnemyStates.dead;
                m_gameModel.EnemyList.Remove(this);

                //敌人死亡时触发
                m_enemyModel.OnEnemyDead.Trigger(this.transform.position);

                //TODO:逻辑和表现分离
                ResUtil.GenerateGOAsync(destoryFxName, this.transform.position, (go) =>
                {
                    Destroy(this.gameObject);
                });
            }
            else
            {
                m_hp -= (int)Mathf.Ceil(sumDmg);

                StartCoroutine(HitFlash());

                //敌人受伤时触发
                m_enemyModel.OnEnemyBeHit.Trigger(this.transform.position);
            }

            FloatingTextCanvas.ShowFloatingText(
                this.GetModel<SettingModel>().DmgNumEnable, transform.position, sumDmg.ToString());

            m_gameModel.OnCalcDmg.Trigger((int)Mathf.Ceil(sumDmg));
        }

        private IEnumerator HitFlash()
        {
            Body.material.SetFloat("_ColorRange", 1f);
            yield return new WaitForSeconds(0.1f);
            Body.material.SetFloat("_ColorRange", 0f);
        }


        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                this.SendCommand(new PlayerHurtCommand());
            }
        }


    }
}
