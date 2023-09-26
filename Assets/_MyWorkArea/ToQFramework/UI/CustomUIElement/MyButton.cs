using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Car
{
    public class MyButton : Button
    {
        protected override void Start()
        {
            base.Start();

            this.onClick.AddListener(() =>
            {
                AudioKit.PlaySound("UI Click 36");
            });
        }


    }
}