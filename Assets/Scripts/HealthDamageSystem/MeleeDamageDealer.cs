using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bengal.HealthDamageSystem
{
    public class MeleeDamageDealer : MonoBehaviour
    {
        [SerializeField] private bool usedByPlayer;
        [SerializeField] private Transform meleeEffectArea;
        [SerializeField] private LayerMask meleeEffectMask;
        [SerializeField] private bool showWireFrame;

        [Header("These will be moved to data later")]
        [SerializeField] private bool nudgePlayerWhenHit;
        [SerializeField] private float nudgeForceToPlayer;


        public void DoMeleeAttack(int damageAmount)
        {
            RaycastHit2D meleeAreaHit = Physics2D.BoxCast(meleeEffectArea.position, meleeEffectArea.transform.localScale / 2, 0 , Vector2.zero, 0, meleeEffectMask);
            if (meleeAreaHit)
            {
                // Do melee damage to enemy
                if (usedByPlayer)
                {
                    if (meleeAreaHit.collider.GetComponent<EnemyHealthManager>() == null)
                        return;

                    meleeAreaHit.collider.GetComponent<EnemyHealthManager>().HandleRemoveHealth(damageAmount);
                }
                // Do melee damage to player
                else
                {
                    if (meleeAreaHit.collider.GetComponent<PlayerHealthManager>() == null)
                        return;

                    meleeAreaHit.collider.GetComponent<PlayerHealthManager>().HandleRemoveHealth(damageAmount);

                    if (nudgePlayerWhenHit)
                    {
                        meleeAreaHit.collider.GetComponent<Rigidbody2D>().AddForce(Vector2.up * nudgeForceToPlayer);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (!showWireFrame) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(meleeEffectArea.position, meleeEffectArea.transform.localScale / 2);
        }
    }
}
