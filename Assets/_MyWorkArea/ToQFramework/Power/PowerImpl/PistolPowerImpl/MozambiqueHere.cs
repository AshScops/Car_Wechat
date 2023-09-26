namespace QFramework.Car
{
    /// <summary>
    /// 手枪：每次额外射出两发弹丸，但伤害略微降低，散射略微增加
    /// </summary>
    public class MozambiqueHere : PowerBase
    {
        public override int GetPowerId()
        {
            return 101;
        }

        private int ammoCountPerShoot = 2;
        private int atk = -2;
        private int descScatterAngle = -5;
        public override void OnAttach()
        {
            gameModel.FixedAmmoCountPerShoot[typeof(Pistol)] += ammoCountPerShoot;
            gameModel.FixedAtk[typeof(Pistol)] += atk;
            gameModel.FixedScatterAngle[typeof(Pistol)] += descScatterAngle;
        }

        public override void OnUnattach()
        {
            gameModel.FixedAmmoCountPerShoot[typeof(Pistol)] -= ammoCountPerShoot;
            gameModel.FixedAtk[typeof(Pistol)] -= atk;
            gameModel.FixedScatterAngle[typeof(Pistol)] -= descScatterAngle;
        }
    }

}