using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using Bengal.HealthDamageSystem;
using UnityEngine;

namespace Bengal.Scripts.Enemies
{

    public class EnemySensor : MonoBehaviour
    {
        public LayerMask       targetLayer;
        public float           targetCheckLenght;
        
        public bool            isGrounded;
        public LayerMask       groundLayer;
        public float           groundCheckLenght;
        
        public bool            wallHit;
        public float           wallCheckLenght;
        public LayerMask       wallCheckLayer;
        
        public bool            groundEndHit;
        public float           groundCheckOffset;
        public float           groundEndCheckLenght;
        
        public bool            isSloping;
        public bool            isDownSloping;
        public float           upVelocityWhenSloping;
        public float           slopeCheckOffsetY;
        public float           slopeCheckLenght;
        public LayerMask       slopeCheckLayer;

        public bool            isClearingTarget;
        
        public bool GroundCheck()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght, groundLayer);
            return isGrounded;
        }

        public bool WallCheck()
        {
            Vector3 fwd = transform.TransformDirection(Vector2.right * transform.localScale.x);
            wallHit = Physics2D.Raycast(transform.position, fwd, wallCheckLenght, wallCheckLayer);
            return wallHit;
        }
        
        public bool SlopeCheck()
        {
            Vector3 fwd = transform.TransformDirection(Vector2.right * transform.localScale.x);
            isSloping = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y + slopeCheckOffsetY), fwd, slopeCheckLenght, slopeCheckLayer );
            return isSloping;
            
        }
        
        public bool DownSlopeCheck()
        {
            Vector3 fwd = transform.TransformDirection(Vector2.left * transform.localScale.x);
            isDownSloping = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y + slopeCheckOffsetY), fwd, slopeCheckLenght, slopeCheckLayer );
            return isDownSloping;
            
        }

        public bool GroundEndCheck()
        {
            var offset = groundCheckOffset * transform.localScale.x;
            groundEndHit = Physics2D.Raycast(new Vector2(transform.position.x + offset, transform.position.y), Vector2.down, groundEndCheckLenght, groundLayer);
            return groundEndHit;
        }
        
        public bool TargetCheck()
        {
            Vector3 fwd = transform.TransformDirection(Vector2.right * transform.localScale.x);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd, targetCheckLenght, targetLayer);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<PlayerHealthManager>())
            {
                if(!GetComponent<EnemyBaseStateMachine>().target)
                {
                    isClearingTarget = false;
                    GetComponent<EnemyBaseStateMachine>().SetTarget(hit.collider.gameObject);  
                }
                return true;
            }
            if(GetComponent<EnemyBaseStateMachine>().target && !isClearingTarget)
            {
                isClearingTarget = true;
                GetComponent<EnemyBaseStateMachine>().HandleClearTarget();  
            }
            
            return false;
        }
        void OnDrawGizmosSelected()
        {
            //SlopeCheckLine
            Vector3 fwd = transform.TransformDirection(Vector2.right * transform.localScale.x);
            // Draws a blue line from this transform to the target
            Vector3 slopeStartPos = new Vector3(transform.position.x, transform.position.y + slopeCheckOffsetY,
                transform.position.z);
            Vector3 slopeTargetPos = new Vector3(transform.position.x + slopeCheckLenght,
                transform.position.y + slopeCheckOffsetY, transform.position.z);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(slopeStartPos,slopeTargetPos);
            
            //DownSlopeLine
            Vector3 fwd2 = transform.TransformDirection(Vector2.left * -transform.localScale.x);
            // Draws a blue line from this transform to the target
            Vector3 downSlopeStartPos = new Vector3(transform.position.x, transform.position.y + slopeCheckOffsetY,
                transform.position.z);
            Vector3 downSlopeTargetPos = new Vector3(transform.position.x + slopeCheckLenght,
                transform.position.y + slopeCheckOffsetY, transform.position.z);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(downSlopeStartPos,downSlopeTargetPos);
            
            // Draws a blue line from this transform to the target
            Vector3 groundStartPos = new Vector3(transform.position.x, transform.position.y,
                transform.position.z);
            Vector3 groundTargetPos = new Vector3(transform.position.x,transform.position.y - groundCheckLenght, transform.position.z);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundStartPos,groundTargetPos);
            
            
        }
        
    }
}
