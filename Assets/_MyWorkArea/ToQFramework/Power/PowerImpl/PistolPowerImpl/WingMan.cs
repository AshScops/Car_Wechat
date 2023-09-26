using System.Collections;

namespace QFramework.Car
{
    /// <summary>
    /// 手枪：单发伤害提高
    /// </summary>
    public class WingMan : PowerBase
    {
        public override int GetPowerId()
        {
            return 102;
        }

        private int atk = 5;
        public override void OnAttach()
        {
            gameModel.FixedAtk[typeof(Pistol)] += atk;
        }

        public override void OnUnattach()
        {
            gameModel.FixedAtk[typeof(Pistol)] -= atk;
        }

    }
}