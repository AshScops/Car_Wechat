namespace QFramework.Car
{
    public class Uzi : ShootWeapon
    {
        protected override void InitAmmo()
        {
            ResUtil.LoadPrefabAsync("Uzi Ammo", (prefab) =>
            {
                this.ammoPrefab = prefab;
            });
        }

    }
}