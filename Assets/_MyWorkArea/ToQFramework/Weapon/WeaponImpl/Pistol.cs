namespace QFramework.Car
{
    public class Pistol : ShootWeapon
    {
        protected override void InitAmmo()
        {
            ResUtil.LoadPrefabAsync("Pistol Ammo", (prefab) =>
            {
                this.ammoPrefab = prefab;
            });
        }

    }

}
