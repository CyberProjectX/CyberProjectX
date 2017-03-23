using UnityEngine;

namespace Scripts.Client.Components.TargetableComponents
{ 
    public class HealthComponent : BaseComponent
    {
        public int MaxHealth = 100;
        public int CurrentHealth = 100;
        public bool IsAlive = true;
        public bool IsDead = false;

        public BaseDieComponent[] DieComponents;

        protected void Update()
        {
            if (!IsAlive && !IsDead)
            {
                if(DieComponents != null)
                {
                    foreach(var component in DieComponents)
                    {
                        component.OnDie();
                    }
                }

                IsDead = true;
            }
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;

            if(CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

            if(IsAlive && CurrentHealth == 0)
            {
                IsAlive = false;
            }
        }
    }
}
