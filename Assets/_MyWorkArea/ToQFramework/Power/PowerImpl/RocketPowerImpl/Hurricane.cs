using System;

namespace QFramework.Car
{
    /// <summary>
    /// ���Ͳ���������̶��ӳ�30������������������װ��ʱ�䣬���˺���������
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