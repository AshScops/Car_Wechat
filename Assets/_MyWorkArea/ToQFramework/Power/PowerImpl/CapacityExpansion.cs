
/// <summary>
/// µ¯¼ÐÈÝÁ¿·­±¶
/// </summary>
namespace QFramework.Car
{
    public class CapacityExpansion : PowerBase
    {
        public override int GetPowerId()
        {
            return 2;
        }

        public override void OnAttach()
        {
            gameModel.SkillCapacityRate.Value *= 2f;
        }

        public override void OnUnattach()
        {
            gameModel.SkillCapacityRate.Value /= 2f;
        }


    }
}