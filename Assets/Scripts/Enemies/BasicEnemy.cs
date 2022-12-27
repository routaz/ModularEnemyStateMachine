using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bengal.Enemies
{
    public class BasicEnemy : MonoBehaviour
    {
        public float moveSpeed;
        //public float attackSpeed;
        public float jumpForce;

        [HideInInspector]
        public Rigidbody2D rb2D;
        [HideInInspector]
        public Collider2D myCollider;

        //TODO create getters and setters?
        private bool isGrounded;
        public LayerMask groundLayer;
        public float groundCheckLenght;

        private bool wallHit;
        private bool isFlipped;
        public bool isFalling;

        public float wallCheckLenght;
        public LayerMask wallCheckLayer;

        private bool groundEndHit;
        public float groundCheckOffset;
        public float groundEndCheckLenght;

        Vector2 movementVelocity;

        public void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            rb2D = GetComponent<Rigidbody2D>();
            myCollider = GetComponent<Collider2D>();
        }

        public void Update()
        {
            if(isGrounded)
                isFalling = false;
            else
                isFalling = true;

            if (!groundEndHit && !isFlipped && !isFalling || wallHit && !isFlipped && !isFalling)
            {
                isFlipped = true;
                Flip();
            }
         
        }

        public void FixedUpdate()
        {
            movementVelocity = Vector2.right * Time.deltaTime * moveSpeed;
            if (!isGrounded)
            {
                isFalling = true;
                movementVelocity.y += Physics2D.gravity.y;
            }
        
            rb2D.velocity = movementVelocity;

            GroundCheck();
            WallCheck();
            GroundEndCheck();
        }

        public void LateUpdate()
        {

        }

        private void Flip()
        {
            moveSpeed = -moveSpeed;
            Vector3 flipScale = transform.localScale;
            flipScale.x *= -1;
            transform.localScale = flipScale;
            isFlipped = false;

        }

        public bool GroundCheck()
        {
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght, groundLayer) ? true : false;
            return isGrounded;
        }

        public bool WallCheck()
        {
            Vector3 fwd = transform.TransformDirection(Vector2.right * transform.localScale.x);
            wallHit = Physics2D.Raycast(transform.position, fwd, wallCheckLenght, wallCheckLayer) ? true : false;
            return wallHit;
        }

        public bool GroundEndCheck()
        {
            var offset = groundCheckOffset * transform.localScale.x;
            groundEndHit = Physics2D.Raycast(new Vector2(transform.position.x + offset, transform.position.y), Vector2.down, groundCheckLenght, groundLayer) ? true : false;
            return groundEndHit;

        }
    }
}
