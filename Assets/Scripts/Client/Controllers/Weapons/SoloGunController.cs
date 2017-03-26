using Scripts.Client.Contexts;
using Scripts.Client.Controllers.Projectiles;
using Scripts.Common;
using Scripts.Common.ObjectPools;
using System;
using UnityEngine;

namespace Scripts.Client.Controllers.Weapons
{
    public class SoloGunController : BaseWeaponController
    {
        public override void Fire()
        {
            var offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);

            var muzzle = ObjectPoolManager.Local.Create(Consts.Prefab.Weapons.SoloGun.Muzzle, transform.position, transform.rotation);
            ObjectPoolManager.Local.Return(muzzle, 0.06f);

            var projectileObject = ObjectPoolManager.Local.Create(Consts.Prefab.Weapons.SoloGun.Projectile, transform.position + transform.forward, offset * transform.rotation);
            var projectileController = projectileObject.GetComponent<SoloGunProjectileController>();
            if (projectileController)
            {
                projectileController.SetWeaponController(this);
                projectileController.SetOffset(0f);//soloGunOffset = 0f
            }

            GameContext.Current.Audio.Weapon.ShotSoloGun(transform.position);
        }

        public override void MakeImpact(Vector3 position)
        {
            var impact = ObjectPoolManager.Local.Create(Consts.Prefab.Weapons.SoloGun.Impact, position);
            ObjectPoolManager.Local.Return(impact, 1f);

            GameContext.Current.Audio.Weapon.HitSoloGun(position);
        }
    }
}
