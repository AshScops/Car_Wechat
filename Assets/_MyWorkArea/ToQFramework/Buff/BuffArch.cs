using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class BuffArch : Architecture<BuffArch>
    {
        protected override void Init()
        {
            this.RegisterModel<BuffModel>(new BuffModel());
        }


    }
}