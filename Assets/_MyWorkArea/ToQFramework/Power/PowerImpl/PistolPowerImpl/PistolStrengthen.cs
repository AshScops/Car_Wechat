using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    /// <summary>
    /// 对手枪提供全属性加成
    /// </summary>
    public class PistolStrengthen : PowerBase
    {
        public override int GetPowerId()
        {
            return 100;
        }

        private readonly int descScatterAngle = 20;
        private readonly int atk = 5;
        private readonly int capacity = 3;
        private readonly float fire = -0.18f;
        private readonly float reloaded = -0.5f;

        public override void OnAttach()
        {
            gameModel.FixedScatterAngle[typeof(Pistol)] += descScatterAngle;
            gameModel.FixedAtk[typeof(Pistol)] += atk;
            gameModel.FixedCapacity[typeof(Pistol)] += capacity;
            gameModel.FixedFireInterval[typeof(Pistol)] += fire;
            gameModel.FixedReloadInterval[typeof(Pistol)] += reloaded;
        }

        public override void OnUnattach()
        {
            gameModel.FixedScatterAngle[typeof(Pistol)] -= descScatterAngle;
            gameModel.FixedAtk[typeof(Pistol)] -= atk;
            gameModel.FixedCapacity[typeof(Pistol)] -= capacity;
            gameModel.FixedFireInterval[typeof(Pistol)] -= fire;
            gameModel.FixedReloadInterval[typeof(Pistol)] -= reloaded;
        }
    }
}