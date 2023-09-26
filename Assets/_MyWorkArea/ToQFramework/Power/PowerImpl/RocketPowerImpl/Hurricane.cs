using System;

namespace QFramework.Car
{
    /// <summary>
    /// 火箭筒：备弹量固定加成30，显著缩短射击间隔和装弹时间，但伤害显著降低
    /// </summary>
    public class Hurricane : PowerBase
    {
        public override int GetPowerId()
        {
            return 109;
        }


        private Type weaponType = typeof(RocketLauncher);
        private int capacity = 30;
        private float fireInterval = -0.80f;
        private float reloadInterval = -0.5f;
        private float atk = -25f;
        public override void OnAttach()
        {
            gameModel.FixedCapacity[weaponType] += capacity;
            gameModel.FixedFireInterval[weaponType] += fireInterval;
            gameModel.FixedReloadInterval[weaponType] += reloadInterval;
            gameModel.FixedAtk[weaponType] += atk;
            gameModel.ResetCapacity.Trigger();
        }

        public override void OnUnattach()
        {
            gameModel.FixedCapacity[weaponType] -= capacity;
            gameModel.FixedFireInterval[weaponType] -= fireInterval;
            gameModel.FixedReloadInterval[weaponType] -= reloadInterval;
            gameModel.FixedAtk[weaponType] -= atk;
            gameModel.ResetCapacity.Trigger();
        }
    }
}