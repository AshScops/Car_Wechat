using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class GetScoreCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<GameModel>().Score.Value++;

        }
    }
}