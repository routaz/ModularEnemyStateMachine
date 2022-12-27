using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bengal.HealthDamageSystem
{
    public class HealthGiver : MonoBehaviour
    {
        [SerializeField] private int _healthGiven;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerHealthManager>() == null)
                return;

            collision.GetComponent<PlayerHealthManager>().HandleAddHealth(_healthGiven);
        }
    }
}
