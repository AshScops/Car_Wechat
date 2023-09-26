using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.Car
{
    /// <summary>
    /// ��������20%��ȫ������������10%
    /// </summary>
    public class MoveFaster : PowerBase
    {
        public override int GetPowerId()
        {
            return 6;
        }

        public override void OnAttach()
        {
            gameModel.SkillPlayerMoveRate.Value *= 1.2f;
            gameModel.SkillFireRate.Value *= 1.1f;
        }

        public override void OnUnattach()
        {
            gameModel.SkillPlayerMoveRate.Value /= 1.2f;
            gameModel.SkillFireRate.Value /= 1.1f;
        }
    }
}