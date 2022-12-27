using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bengal.HealthDamageSystem
{
    public abstract class HealthBaseClass : MonoBehaviour
    {
        public abstract int CurrentHealth { get ; set; } 
        public abstract int MaxHealth { get; set; }
        
        public int currentHealth;
        public int maxHealth;

        protected virtual void Init()
        {    
            currentHealth = MaxHealth;
        }

        public virtual void Update()
        {
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                KillToon();
            }
        }

        protected virtual void RemoveHealth(int healthRemoved)
        {
            if(currentHealth > 0)
            {
                currentHealth -= healthRemoved;
            }
            else
            {
                KillToon();
            }
        }

        protected virtual void AddHealth(int healthAdded)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += healthAdded;
            }
            else
            {
                currentHealth = maxHealth;
            }
        }
        
        protected virtual void Respawn()
        {
            currentHealth = maxHealth;
        }
        
        protected virtual void KillToon()
        {
            currentHealth = 0;
        }
      

    }
}
