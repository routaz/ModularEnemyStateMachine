using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bengal.HealthDamageSystem
{
    public class EnemyHealthManager : HealthBaseClass
    {
        public override int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public override int MaxHealth { get => maxHealth; set => maxHealth = value; }

        private void Start()
        {
            HandleInit();

        }
        private void HandleInit()
        {
            base.Init();

        }
        public override void Update()
        {
            base.Update();
        }

        public void HandleAddHealth(int healthAdded)
        {
            base.AddHealth(healthAdded);
        }

        public void HandleKillToon()
        {
            base.KillToon();
        }

        public void HandleRemoveHealth(int healthRemoved)
        {
            base.RemoveHealth(healthRemoved);
        }

        public void HandleRespawn()
        {
            base.Respawn();
        }
        void OnGUI()
        {
            GUI.Label(new Rect(10, 30, 200, 20), "EnemyHealth" + CurrentHealth.ToString());
        }
    }
}
