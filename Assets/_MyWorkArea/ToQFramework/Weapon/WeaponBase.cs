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
        /// 检测半径
        /// </summary>
        [SerializeField]
        protected float radius = 7f;

        /// <summary>
        /// Execute间隔
        /// </summary>
        [SerializeField]
        protected float fireCd;

        protected CooldownTimer fireCdTimer;

        /// <summary>
        /// 装弹CD
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
            if (!reloadCdTimer.CoolDownOnUpdate(Time.deltaTime))//换弹是否完成
            {
                return;
            }
            else if (this.currentCapacity <= 0)//换弹Cd转完了，补满子弹
            {
                this.currentCapacity = ValueCalculateCenter.GetCapacity(this.rawCapacity, this.GetType());
                return;
            }

            if (fireCdTimer.CoolDownOnUpdate(Time.deltaTime))//开火是否冷却完毕
            {
                WeaponExecute();
                //武器攻击时生效
                this.GetModel<WeaponModel>().OnWeaponExecute.Trigger();

                if (this.currentCapacity <= 0)//空弹夹，通知换弹
                {
                    AudioKit.PlaySound("Weapon_Shotgun_Reload");
                    reloadCdTimer.ResetCD(ValueCalculateCenter.GetReloadCd(this.ReloadCd, this.GetType()));
                    //换弹时触发
                    this.GetModel<WeaponModel>().OnWeaponReload.Trigger(); 
                }
            }
        }

        /// <summary>
        /// 冷却结束自动执行，仅触发一次
        /// </summary>
        /// <returns>成功执行返回true，否则返回false</returns>
        public abstract bool WeaponExecute();


    }
}
