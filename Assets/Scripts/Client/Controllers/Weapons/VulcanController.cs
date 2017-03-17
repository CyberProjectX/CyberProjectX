using Forge3D;
using Scripts.Common.ObjectPools;
using UnityEngine;

namespace Scripts.Client.Controllers.Weapons
{
    // todo: remove F3DAudioController and F3DPoolManager
    // todo: replace ObjectPoolManager.Instance.CreateSingle to Create
    public class VulcanController : BaseWeaponController
    {
        public override void Start()
        {
            FireRate = 0.5f;
        }

        public override void Fire()
        {
            base.Fire();
            FireInternal();
        }

        public void WeaponImpact(Vector3 position)
        {
            // Spawn impact prefab at specified position
            F3DPoolManager.Pools["GeneratedPool"].Spawn(Impact.transform, position, Quaternion.identity, null);
            // Play impact sound effect
            F3DAudioController.instance.VulcanHit(position);
        }

        private void FireInternal()
        {
            // Get random rotation that offset spawned projectile
            var offset = Quaternion.Euler(Random.onUnitSphere);

            var pool = F3DPoolManager.Pools["GeneratedPool"];

            // Spawn muzzle flash and projectile with the rotation offset at current socket position
            pool.Spawn(Muzzle.transform, transform.position, transform.rotation, transform);
            var projectileObject = ObjectPoolManager.Instance.CreateSingle(Projectile, transform.position + transform.forward, offset * transform.rotation);
            //var projectileTransform = pool.Spawn(Projectile, transform.position + transform.forward, offset * transform.rotation, null);
            //var projectileObject = projectileTransform.gameObject;

            var projectile = projectileObject.gameObject.GetComponent<F3DProjectile>();
            if (projectile)
            {
                projectile.SetOffset(0); // vulcanOffset = 0
            }

            //// Emit one bullet shell
            //if (ShellParticles.Length > 0)
            //    ShellParticles[curSocket].Emit(1);

            // Play shot sound effect
            F3DAudioController.instance.VulcanShot(transform.position);
        }
    }
}
