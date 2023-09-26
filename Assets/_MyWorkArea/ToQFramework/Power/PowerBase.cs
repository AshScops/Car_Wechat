
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
        /// ��д�˷���ʹ�������Id��json���е�Idһһ��Ӧ
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