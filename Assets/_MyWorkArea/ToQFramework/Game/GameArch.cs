using UnityEngine;

namespace QFramework.Car
{
    public class GameArch : Architecture<GameArch>
    {
        protected override void Init()
        {
            ResKit.Init();
            UIKit.Config.Root.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, 0.5f);

            //Models
            this.RegisterModel(new GameModel());
            this.RegisterModel(new PlayerModel());
            this.RegisterModel(new EnemyModel());
            this.RegisterModel(new WeaponModel());
            this.RegisterModel(new ItemModel());
            this.RegisterModel(new AchieveModel());
            this.RegisterModel(new SettingModel()); 

            //Systems
            this.RegisterSystem(new GameSystem());
            this.RegisterSystem(new AchieveSystem());

            //Utilities


        }




    }

}
