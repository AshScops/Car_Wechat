using UnityEngine;


namespace QFramework.Car
{
    public class CooldownTimer
    {
        private float m_cd;

        public CooldownTimer() { }

        public CooldownTimer(float Cd)
        {
            this.m_cd = Cd;
        }

        /// <summary>
        /// 冷却完毕才返回true，你需要在Update中调用它
        /// </summary>
        /// <returns></returns>
        public bool CoolDownOnUpdate(float deltaTime)
        {
            if (this.m_cd - deltaTime <= 0)
            {
                this.m_cd = 0;
                return true;
            }
            else
            {
                this.m_cd -= deltaTime;
                return false;
            }
        }

        public void ResetCD(float Cd)
        {
            this.m_cd = Cd;
        }

        public bool GetCoolDownResult()
        {
            return this.m_cd == 0;
        }


    }
}