using System;

namespace QFramework.Car
{
    /// <summary>
    /// È«ÎäÆ÷¹á´©+1
    /// </summary>
    public class PointedBullet : PowerBase
    {
        public override int GetPowerId()
        {
            return 10;
        }

        private Type[] weaponTypes = new Type[] { typeof(Pistol), typeof(Uzi), typeof(Shotgun), typeof(RocketLauncher) };
        public override void OnAttach()
        {
            for(int i = 0; i < weaponTypes.Length; i++)
            {
                gameModel.FixedPierce[weaponTypes[i]] += 1;
            }
        }

        public override void OnUnattach()
        {
            for (int i = 0; i < weaponTypes.Length; i++)
            {
                gameModel.FixedPierce[weaponTypes[i]] -= 1;
            }
        }
    }
}