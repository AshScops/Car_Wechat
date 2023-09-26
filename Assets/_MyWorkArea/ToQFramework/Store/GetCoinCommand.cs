using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class GetCoinCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<ItemModel>().Coin.Value++;

        }


    }
}