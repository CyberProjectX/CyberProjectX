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

            var muzzle = ObjectPoolManager.Instance.Create(Consts.Prefab.Weapons.SoloGun.Muzzle, Muzzle, transform.position, transform.rotation);
            //F3DPoolManager.Pools["GeneratedPool"].Spawn(soloGunMuzzle, TurretSocket[curSocket].position, TurretSocket[curSocket].rotation, TurretSocket[curSocket]);
            ObjectPoolManager.Instance.Return(muzzle, 0.06f);

            var projectileObject = ObjectPoolManager.Instance.Create(Consts.Prefab.Weapons.SoloGun.Projectile, Projectile, transform.position + transform.forward, offset * transform.rotation);
            var projectileController = projectileObject.GetComponent<SoloGunProjectileController>();
            //GameObject newGO =F3DPoolManager.Pools["GeneratedPool"].Spawn(soloGunProjectile, TurretSocket[curSocket].position + TurretSocket[curSocket].forward, offset * TurretSocket[curSocket].rotation, null).gameObject as GameObject;
            //F3DProjectile proj = newGO.GetComponent<F3DProjectile>();
            if (projectileController)
            {
                projectileController.SetWeaponController(this);
                projectileController.SetOffset(0f);//soloGunOffset = 0f
            }

            GameContext.Current.Audio.Weapon.ShotSoloGun(transform.position);
            //F3DAudioController.instance.SoloGunShot(TurretSocket[curSocket].position);
        }

        public override void MakeImpact(Vector3 position)
        {
            var impact = ObjectPoolManager.Instance.Create(Consts.Prefab.Weapons.SoloGun.Impact, Impact, position);
            ObjectPoolManager.Instance.Return(impact, 1f);

            GameContext.Current.Audio.Weapon.HitSoloGun(position);

            // Spawn impact prefab at specified position
            //F3DPoolManager.Pools["GeneratedPool"].Spawn(vulcanImpact, pos, Quaternion.identity, null);
            // Play impact sound effect
            //F3DAudioController.instance.VulcanHit(pos);


        }
    }
}
