
namespace QFramework.Car
{
    /// <summary>
    /// ȫ������������50%
    /// </summary>
    public class FireFaster : PowerBase
    {
        public override int GetPowerId()
        {
            return 4;
        }

        public override void OnAttach()
        {
            gameModel.SkillFireRate.Value *= 1.5f;
        }

        public override void OnUnattach()
        {
            gameModel.SkillFireRate.Value /= 1.5f;
        }
    }
}