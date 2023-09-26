using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class TenHearts : AchieveBase
    {

        private PlayerModel m_playerModel;

        public override void DetectCondition()
        {
            m_playerModel = GameArch.Interface.GetModel<PlayerModel>();
            m_playerModel.Hp.Register(Callback);
        }

        public void Callback(int hp)
        {
            if(hp >= 10)
            {
                m_playerModel.Hp.UnRegister(Callback);
                Unlock();
            }
        }

        public override int GetAchieveId()
        {
            return 7;
        }


    }

}
