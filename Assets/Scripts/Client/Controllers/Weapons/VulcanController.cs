using Forge3D;
using Scripts.Client.Contexts;
using Scripts.Client.Controllers.Projectiles;
using Scripts.Common;
using Scripts.Common.ObjectPools;
using UnityEngine;

namespace Scripts.Client.Controllers.Weapons
{
    public class VulcanController : BaseWeaponController
    {
        public override void Fire()
        {
            var offset = Quaternion.Euler(Random.onUnitSphere);

            var muzzle = ObjectPoolManager.Instance.Create(Consts.Prefab.Weapons.Vulcan.Muzzle, Muzzle, transform.position, transform.rotation);
            ObjectPoolManager.Instance.Return(muzzle, 0.05f);

            var projectileObject = ObjectPoolManager.Instance.Create(Consts.Prefab.Weapons.Vulcan.Projectile, Projectile, transform.position + transform.forward, offset * transform.rotation);

            var projectileController = projectileObject.gameObject.GetComponent<VulcanProjectileController>();
            if (projectileController)
            {
                projectileController.SetWeaponController(this);
                projectileController.SetOffset(0); // vulcanOffset = 0
            }

            //// Emit one bullet shell
            //if (ShellParticles.Length > 0)
            //    ShellParticles[curSocket].Emit(1);

            GameContext.Current.Audio.Weapon.ShotVulcan(transform.position);
        }

        public override void MakeImpact(Vector3 position)
        {
            var impact = ObjectPoolManager.Instance.Create(Consts.Prefab.Weapons.Vulcan.Impact, Impact, position);
            ObjectPoolManager.Instance.Return(impact, 1f);

            GameContext.Current.Audio.Weapon.HitVulcan(position);
        }
    }
}
