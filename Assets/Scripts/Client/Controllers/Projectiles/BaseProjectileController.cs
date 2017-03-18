using System;
using Scripts.Common.ObjectPools;
using UnityEngine;
using Scripts.Client.Controllers.Weapons;

namespace Scripts.Client.Controllers.Projectiles
{
    public abstract class BaseProjectileController : BaseController, IObjectPoolCreatable
    {
        public LayerMask LayerMask;
        public float LifeTime = 5f;
        public float Velocity = 300f;

        public float RemoveDelay = 0f;
        public bool RemoveWithDelay = false;// can we work with RemoveDelay only?
        public ParticleSystem[] delayedParticles;        

        private float raycastAdvance = 2f;

        private ParticleSystem[] particles;
        private RaycastHit hitPoint;
        private bool isHit;
        private bool isFXSpawned;
        private float timer;
        protected float FxOffset;
        protected BaseWeaponController WeaponController;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public virtual void Awake()
        {
            particles = GetComponentsInChildren<ParticleSystem>();
        }

        public virtual void MakeImpact(RaycastHit hitPoint)
        {
        }

        public virtual void Update()
        {
            if (isHit)
            {
                OnHit();                
            }
            else
            {
                OnNonCollision();
            }

            timer += Time.deltaTime;
        }

        public void OnEnable()
        {
            isHit = false;
            isFXSpawned = false;
            timer = 0f;
            hitPoint = new RaycastHit();
        }

        public void OnDisable()
        {
        }

        public void SetOffset(float offset)
        {
            FxOffset = offset;
        }

        public void SetWeaponController(BaseWeaponController weaponController)
        {
            WeaponController = weaponController;
        }

        protected void ApplyForce(float force)
        {
            if (hitPoint.rigidbody != null)
            {
                hitPoint.rigidbody.AddForceAtPosition(transform.forward * force, hitPoint.point, ForceMode.VelocityChange);
            }
        }

        private void OnHit()
        {
            // Execute once
            if (!isFXSpawned)
            {
                MakeImpact(hitPoint);

                isFXSpawned = true;
            }

            // Despawn current projectile 
            if (!RemoveWithDelay || (RemoveWithDelay && (timer >= RemoveDelay)))
            {
                ReturnToObjectPool();
            }
        }

        private void OnNonCollision()
        {
            Vector3 step = transform.forward * Time.deltaTime * Velocity;

            if (Physics.Raycast(transform.position, transform.forward, out hitPoint, step.magnitude * raycastAdvance, LayerMask))
            {
                isHit = true;

                if (RemoveWithDelay)
                {
                    timer = 0f;
                    Delay();
                }
            }
            else if (timer >= LifeTime)
            {
                ReturnToObjectPool();
            }

            transform.position += step;
        }

        private void Delay()
        {
            if (particles.Length > 0 && delayedParticles.Length > 0)
            {
                bool delayed;
                for (int i = 0; i < particles.Length; i++)
                {
                    delayed = false;
                    for (int y = 0; y < delayedParticles.Length; y++)
                    {
                        if (particles[i] == delayedParticles[y])
                        {
                            delayed = true;
                            break;
                        }
                    }
                    particles[i].Stop(false);
                    if (!delayed)
                    {
                        particles[i].Clear(false);
                    }
                }
            }
        }

        private void ReturnToObjectPool()
        {
            ObjectPoolManager.Instance.Return(gameObject);
        }
    }
}
