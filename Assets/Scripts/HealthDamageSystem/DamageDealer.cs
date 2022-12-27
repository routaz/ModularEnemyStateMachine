using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bengal.HealthDamageSystem
{
    public class DamageDealer : MonoBehaviour
    {
        [Tooltip("Check when used by Player targetting Enemy")]
        [SerializeField] private bool _usedByPlayer;
        [Tooltip("Amount of Damage to inflict")]
        [SerializeField] private int _damageAmount;
        [HideInInspector] public int setDamage; // Used by PlayerSpellProjectile
        //public SpellTypes.SpellType spellType; // Used by PlayerSpellProjectile, will be either Earth, Fire, Water or Wind
        [Tooltip("If checked, will DESTROY THIS OBJECT on collision")]
        public bool destroyOnContact;
        [Tooltip("If in contact, what is the interval we will trigger 'RemoveHealth' from contact")]
        [SerializeField] private float collisionDamageIntervalInSeconds;
        private GameObject _target;
        private bool canDamage = true;

        private void Start()
        {
            if (setDamage > 0)
            {
                _damageAmount = setDamage;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Do damage to enemy
            if (_usedByPlayer)
            {
                if (collision.GetComponent<EnemyHealthManager>() == null)
                    return;

                collision.GetComponent<EnemyHealthManager>().HandleRemoveHealth(_damageAmount);

                if (destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
            // Do damage to player
            else
            {
                if (collision.GetComponent<PlayerHealthManager>() == null)
                    return;

                collision.GetComponent<PlayerHealthManager>().HandleRemoveHealth(_damageAmount);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100f);
                if (destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerHealthManager>() == null)
                return;

            if (canDamage)
            {
                _target = collision.gameObject;
                StartCoroutine(HandeleCollisionDamage());
            }

        }

        IEnumerator HandeleCollisionDamage()
        {
            _target.GetComponent<PlayerHealthManager>().HandleRemoveHealth(_damageAmount);
            canDamage = false;
            yield return new WaitForSeconds(collisionDamageIntervalInSeconds);
            canDamage = true;
            _target = null;
        }
    }
}
