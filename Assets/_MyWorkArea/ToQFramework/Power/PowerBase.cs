
namespace QFramework.Car
{
    public abstract class PowerBase
    {
        protected GameModel gameModel;
        protected WeaponModel weaponModel;

        public PowerBase()
        {
            gameModel = GameArch.Interface.GetModel<GameModel>();
            weaponModel = GameArch.Interface.GetModel<WeaponModel>();
        }

        /// <summary>
        /// 重写此方法使派生类的Id与json表中的Id一一对应
        /// </summary>
        /// <returns></returns>
        public abstract int GetPowerId();

        public abstract void OnAttach();

        public abstract void OnUnattach();

        public virtual void OnUpdate()
        {

        }
    }   
}