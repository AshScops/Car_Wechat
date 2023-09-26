using UnityEngine;


namespace QFramework.Car
{
	public abstract class WeaponBase : ViewController, IController
	{
        [SerializeField]
        protected float atk = 10;

        [SerializeField]
        protected float ammoSpeed = 15f;

        /// <summary>
        /// ���뾶
        /// </summary>
        [SerializeField]
        protected float radius = 7f;

        /// <summary>
        /// Execute���
        /// </summary>
        [SerializeField]
        protected float fireCd;

        protected CooldownTimer fireCdTimer;

        /// <summary>
        /// װ��CD
        /// </summary>
        [SerializeField]
        private float reloadCd;

        protected CooldownTimer reloadCdTimer;

        [SerializeField]
        protected int rawCapacity = 7;

        protected int currentCapacity;


        public float ReloadCd { get => reloadCd; set => reloadCd = value; }


        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        protected virtual void Awake()
        {
            fireCdTimer = new CooldownTimer(ValueCalculateCenter.GetFireCd(this.fireCd, this.GetType()));
            reloadCdTimer = new CooldownTimer(0);
            this.currentCapacity = ValueCalculateCenter.GetCapacity(this.rawCapacity, this.GetType());
        }

        protected void Update()
        {
            if (this.GetModel<GameModel>().GameState != GameStates.isRunning) return;

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            if (!reloadCdTimer.CoolDownOnUpdate(Time.deltaTime))//�����Ƿ����
            {
                return;
            }
            else if (this.currentCapacity <= 0)//����Cdת���ˣ������ӵ�
            {
                this.currentCapacity = ValueCalculateCenter.GetCapacity(this.rawCapacity, this.GetType());
                return;
            }

            if (fireCdTimer.CoolDownOnUpdate(Time.deltaTime))//�����Ƿ���ȴ���
            {
                WeaponExecute();
                //��������ʱ��Ч
                this.GetModel<WeaponModel>().OnWeaponExecute.Trigger();

                if (this.currentCapacity <= 0)//�յ��У�֪ͨ����
                {
                    AudioKit.PlaySound("Weapon_Shotgun_Reload");
                    reloadCdTimer.ResetCD(ValueCalculateCenter.GetReloadCd(this.ReloadCd, this.GetType()));
                    //����ʱ����
                    this.GetModel<WeaponModel>().OnWeaponReload.Trigger(); 
                }
            }
        }

        /// <summary>
        /// ��ȴ�����Զ�ִ�У�������һ��
        /// </summary>
        /// <returns>�ɹ�ִ�з���true�����򷵻�false</returns>
        public abstract bool WeaponExecute();


    }
}
