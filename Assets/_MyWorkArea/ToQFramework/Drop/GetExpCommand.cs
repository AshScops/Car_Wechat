using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.Car
{
    public class GetExpCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<PlayerModel>().CurrentExp.Value++;
            AudioKit.PlaySound("Crystal Reward Tick");
        }

    }

}
