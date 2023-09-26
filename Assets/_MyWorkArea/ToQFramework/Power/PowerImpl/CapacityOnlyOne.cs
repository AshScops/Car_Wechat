
namespace QFramework.Car
{
    /// <summary>
    /// ���������ĵ�����������Ϊһ���ӵ�
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