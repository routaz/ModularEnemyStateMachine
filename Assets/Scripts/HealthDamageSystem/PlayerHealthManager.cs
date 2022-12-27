using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bengal.HealthDamageSystem
{
    public class PlayerHealthManager : HealthBaseClass
    {
        public override int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public override int MaxHealth { get => maxHealth ; set => maxHealth = value; }

        public GameObject   respawnPoint;
        public int          timeToRespawn;
      
        private void Start()
        {
            Init();
        }
        private void Init()
        {
            base.Init();
        }
        
        public void Update()
        {
            base.Update();
        }
   
        public void HandleAddHealth(int healthAdded)
        {
            base.AddHealth(healthAdded);
        }
        
        public void HandleRemoveHealth(int healthRemoved)
        {
            base.RemoveHealth(healthRemoved);
        }

        protected override void KillToon()
        {
            base.KillToon();
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Respawn();
        }
        
        public void SetCurrentRespawnPoint(GameObject newRespawnPoint)
        {
            respawnPoint = newRespawnPoint;
        }

        protected override void Respawn()
        {
            StartCoroutine(HandleRespawn());
        }

        IEnumerator HandleRespawn()
        {
            yield return new WaitForSeconds(timeToRespawn);
            // Using this to not mess up the z-axis
            
            var respawnPosition = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.position = respawnPosition;
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            base.Respawn();
        }
        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Health" + CurrentHealth.ToString());
        }
        
    }
}
