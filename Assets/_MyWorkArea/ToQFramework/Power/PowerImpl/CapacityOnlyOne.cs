
namespace QFramework.Car
{
    /// <summary>
    /// 所有武器的弹夹容量锁定为一发子弹
    /// </summary>
    public class CapacityOnlyOne : PowerBase
    {
        public override int GetPowerId()
        {
            return 3;
        }

        public override void OnAttach()
        {
            gameModel.SkillCapacityOnlyOne.Value = true;
        }

        public override void OnUnattach()
        {
            gameModel.SkillCapacityOnlyOne.Value = false;
        }

    }
}