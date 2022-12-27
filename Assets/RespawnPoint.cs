using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bengal.HealthDamageSystem
{
    public class RespawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerHealthManager>() == null)
                return;

            collision.GetComponent<PlayerHealthManager>().SetCurrentRespawnPoint(gameObject);
        }
    }
}
