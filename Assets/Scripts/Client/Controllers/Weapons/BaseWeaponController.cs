using UnityEngine;

namespace Scripts.Client.Controllers.Weapons
{
    // todo: remove KeyCode.Mouse0
    public abstract class BaseWeaponController : BaseController
    {
        public Transform Projectile;
        public Transform Muzzle;
        public Transform Impact;

        public float FireRate;

        private bool isFiring;
        private float nextFire;

        public virtual void Start()
        {
            nextFire = Time.time;
        }

        public void Update()
        {
            if (!isFiring && Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartFire();
            }

            if (isFiring && Input.GetKeyUp(KeyCode.Mouse0))
            {
                StopFire();
            }

            if (isFiring && Time.time >= nextFire)
            {
                nextFire = Time.time + FireRate;
                Fire();
            }
        }

        public virtual void StartFire()
        {
            isFiring = true;
        }

        public virtual void Fire()
        {

        }

        public virtual void StopFire()
        {
            isFiring = false;
        }

        public virtual void MakeImpact(Vector3 position)
        {
        }
    }
}
