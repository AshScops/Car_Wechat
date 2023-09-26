
namespace QFramework.Car
{
    /// <summary>
    /// 装弹速度提升70%
    /// </summary>
    public class ReloadFaster : PowerBase
    {
        public override int GetPowerId()
        {
            return 7;
        }

        public override void OnAttach()
        {
            gameModel.SkillReloadRate.Value *= 1.7f;
        }

        public override void OnUnattach()
        {
            gameModel.SkillReloadRate.Value /= 1.7f;
        }
    }
}