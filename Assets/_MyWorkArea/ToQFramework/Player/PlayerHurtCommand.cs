using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class PlayerHurtCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            GameArch.Interface.GetModel<PlayerModel>().PlayerHurtEvent.Trigger();
        }

    }
}