namespace QFramework.Car
{
    public class RocketLauncher : ShootWeapon
    {
        protected override void InitAmmo()
        {
            ResUtil.LoadPrefabAsync("Rocket Ammo", (prefab) =>
            {
                this.ammoPrefab = prefab;
            });
        }
    }
}