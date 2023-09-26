using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class TenPowers : AchieveBase
    {
        private PlayerModel m_playerModel;

        public override void DetectCondition()
        {
            m_playerModel = GameArch.Interface.GetModel<PlayerModel>();
            m_playerModel.PlayerPower.GetPowerCnt.Register(Callback);
        }

        private void Callback(int powerCnt)
        {
            if (powerCnt == 10)
            {
                Unlock();

                //½âËøÍêºóÒÆ³ý×¢²á
                m_playerModel.PlayerPower.GetPowerCnt.UnRegister(Callback);
            }
        }

        public override int GetAchieveId()
        {
            return 6;
        }

    }

}
