using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.Car
{
    public class ArmedAllPoints : AchieveBase
    {
        private PlayerWeapon m_playerWeapon;

        public override void DetectCondition()
        {
            m_playerWeapon = GameArch.Interface.GetModel<PlayerModel>().PlayerWeapon;
            m_playerWeapon.WeaponFull.Register(Callback);
        }

        public void Callback(string info)
        {
            m_playerWeapon.WeaponFull.UnRegister(Callback);
            Unlock();
        }


        public override int GetAchieveId()
        {
            return 5;
        }

    }

}

