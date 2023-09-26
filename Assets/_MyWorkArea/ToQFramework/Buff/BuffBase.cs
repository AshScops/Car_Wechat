using System;
using UnityEngine;

namespace QFramework.Car
{
    public abstract class BuffBase : MonoBehaviour
    {
        protected GameModel gameModel;
        public abstract int GetBuffId();

        private float m_durationTime = 5f;
        protected abstract void SetDurationTime(ref float durationTime);
        protected CooldownTimer m_durationTimer;

        private float m_effectTime = 1f;
        protected abstract void SetEffectTime(ref float effectTime);
        protected CooldownTimer m_effectTimer;


        protected int effectCnt = 1;
        protected int effectCntLimit = 5;
        public virtual int GetEffectCnt()
        {
            return effectCnt;
        }

        protected EasyEvent OnBuffGenerate = new EasyEvent();
        public EasyEvent OnBuffOverlay = new EasyEvent();
        public EasyEvent OnBuffEffect = new EasyEvent();
        public EasyEvent OnBuffDestroy = new EasyEvent();


        protected virtual void Awake()
        {
            gameModel = GameArch.Interface.GetModel<GameModel>();
            OnBuffGenerate.Register(OnThisGenerate);
            OnBuffOverlay.Register(OnThisOverlay);
            OnBuffEffect.Register(OnThisEffect);
            OnBuffDestroy.Register(OnThisDestroy);

        }

        protected abstract void OnThisGenerate();
        protected abstract void OnThisOverlay();
        protected abstract void OnThisEffect();
        protected abstract void OnThisDestroy();

        /// <summary>
        /// 重叠时刷新层数
        /// </summary>
        public void OnOverlay()
        {
            m_durationTimer.ResetCD(m_durationTime);
            OnBuffOverlay.Trigger();
        }

        /// <summary>
        /// OnBuffGenerate此时调用
        /// </summary>
        protected void OnEnable()
        {
            SetDurationTime(ref m_durationTime);
            SetEffectTime(ref m_effectTime);
            m_durationTimer = new CooldownTimer(m_durationTime);
            m_effectTimer = new CooldownTimer(m_effectTime);

            OnBuffGenerate.Trigger();
        }

        /// <summary>
        /// OnBuffDestroy此时调用
        /// </summary>
        protected void OnDisable()
        {
            OnBuffDestroy.Trigger();
        }

        protected void OnDestroy()
        {
            OnBuffGenerate.UnRegister(OnThisGenerate);
            OnBuffOverlay.UnRegister(OnThisOverlay);
            OnBuffEffect.UnRegister(OnThisEffect);
            OnBuffDestroy.UnRegister(OnThisDestroy);
        }

        protected void Update()
        {
            if (GameArch.Interface.GetModel<GameModel>().GameState != GameStates.isRunning) return;

            OnUpdate();
        }

        /// <summary>
        /// OnBuffEffect在此调用
        /// </summary>
        protected void OnUpdate()
        {
            if (m_durationTimer.CoolDownOnUpdate(Time.deltaTime))
            {
                m_durationTimer.ResetCD(m_durationTime);
            }

            if (m_effectTimer.CoolDownOnUpdate(Time.deltaTime))
            {
                OnBuffEffect.Trigger();
                m_effectTimer.ResetCD(m_effectTime);
            }
        }



    }
}