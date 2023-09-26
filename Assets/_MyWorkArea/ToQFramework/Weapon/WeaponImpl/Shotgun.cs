namespace QFramework.Car
{
    public class Shotgun : ShootWeapon
    {
        protected override void InitAmmo()
        {
            ResUtil.LoadPrefabAsync("Shotgun Ammo", (prefab) =>
            {
                this.ammoPrefab = prefab;
            });
        }
    }
}