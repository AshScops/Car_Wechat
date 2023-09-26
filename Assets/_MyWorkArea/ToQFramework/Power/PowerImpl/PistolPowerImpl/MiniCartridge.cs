using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// ��ǹ�����������̶��ӳ�50�������������������˺���������
    /// </summary>
    public class MiniCartridge : PowerBase
    {
        public override int GetPowerId()
        {
            return 106;
        }

        private Type weaponType = typeof(Pistol);
        private int capacity = 50;
        private float fireInterval = -0.2f;
        private float atk = -10f;

        public override void OnAttach()
        {
            gameModel.FixedCapacity[weaponType] += capacity;
            gameModel.FixedFireInterval[weaponType] += fireInterval;
            gameModel.FixedAtk[weaponType] += atk;
        }

        public override void OnUnattach()
        {
            gameModel.FixedCapacity[weaponType] -= capacity;
            gameModel.FixedFireInterval[weaponType] -= fireInterval;
            gameModel.FixedAtk[weaponType] -= atk;
        }

    }
}