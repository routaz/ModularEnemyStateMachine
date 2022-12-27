using System;
using System.Collections;
using Bengal.Scripts.Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bengal.Enemies
{
    public class EnemyBaseStateMachine : MonoBehaviour
    {
        //public for debugging reasons.
        public GameObject                target;
        
        //Getters and setters
        public bool                      HasTarget => hasTarget;
        public float                     MoveVelocityX { get => moveVelocityX; set => moveVelocityX = value; }
        public float                     MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float                     JumpForce => jumpForce;
        
        public bool                      IsFalling => isFalling;
        public bool                      IsGrounded => isGrounded;
        public Rigidbody2D               RigidBody => _rigidbody;
        public EnemySensor               Sensor { get; private set; }

        //Handling gravity
        [SerializeField] private float   gravity         = -9.81f;
        [SerializeField] private float   constantGravity = -0.6f;
        [SerializeField] private float   maxGravity      = -15f;
        [SerializeField] private float   gravityScale    = 1f;
        [SerializeField] private float   currentGravity;
        [SerializeField] private Vector2 gravityDirection;
        
        //Handling rigidbody & movement
        private Rigidbody2D              _rigidbody;
        private float                    movementX;
        private float                    movementY;
        [SerializeField] private float   moveDirection = 1;
        
        [SerializeField] private float   moveSpeed;
        [SerializeField] private float   jumpForce;
        private float                    moveVelocityX;
        private float                    moveVelocityY;
        
        //Status Bool checks
        private bool _isFlipped;
        [SerializeField] private bool    isFalling;
        [SerializeField] private bool    isSloping;
        [SerializeField] private bool    isDownSloping;
        [SerializeField] private bool    isJumping;
        [SerializeField] private bool    isGrounded;
        
        private bool                     hasTarget;
        public bool                      isFacingRight;
        public bool                      isStopped;

        //Enemy states
        private IEnemyState              CurrentState { get; set; }
        public IdleState                 idleState;
        public PatrolState               patrolState;
        public AttackState               attackState;
        
        //Wrappers for different states
        [SerializeField] private EnemyIdleStatesWrapper  usedIdleState;
        [SerializeField] private EnemyPatrolStateWrapper usedPatrolsState;
        [SerializeField] private EnemyAttackStateWrapper usedAttacksState;

        //For debugging
        private TextMeshProUGUI          _statusText;//for debugging
        private SpriteRenderer           spriteRenderer;

        private void Awake()
        {
            usedIdleState.Initialize(this);
            usedPatrolsState.Initialize(this);
            usedAttacksState.Initialize(this);
            
            TransitionToState(idleState);
        }

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody     = GetComponent<Rigidbody2D>();
            Sensor         = GetComponent<EnemySensor>();
            _statusText    = GetComponentInChildren<TextMeshProUGUI>();
            
            gravityDirection = Vector2.down;
        }

        private void Update()
        {
            StatusBoolChecks();
            
            CurrentState.Update();
            
            HandleGravity();
            HandleSlopes();

            if (!Sensor.GroundEndCheck() && !_isFlipped && Sensor.GroundCheck() || Sensor.WallCheck() && !_isFlipped && Sensor.GroundCheck())
            {
                if (CurrentState == attackState)
                {
                    isStopped = true;
                }
                else
                {
                    HandleFlip(); 
                }
            }
            else
            {
                isStopped = false;
            }
            
            isFacingRight = transform.localScale.x > 0;
            
            HandleStatesVisualDebug();
        }

        private void StatusBoolChecks()
        {
            isGrounded    = Sensor.GroundCheck();
            isFalling     = !Sensor.GroundCheck() && !Sensor.SlopeCheck() && _rigidbody.velocity.y < 0;
            isSloping     = Sensor.SlopeCheck();
            isDownSloping = Sensor.DownSlopeCheck();
            hasTarget     = Sensor.TargetCheck();
        }
        
        private void HandleStatesVisualDebug()
        {
            //For Debugging reasons
            if (CurrentState == idleState)
            {
                _statusText.text = "Idle";
                spriteRenderer.color = Color.blue;
            }
            else if (CurrentState == patrolState)
            {
                _statusText.text = "Patrol";
                spriteRenderer.color = Color.yellow;
            }
            else if (CurrentState == attackState)
            {
                _statusText.text = "Attack";
                spriteRenderer.color = Color.red;
            }
        }

        private void FixedUpdate()
        {
            CurrentState.FixedUpdate();
            Vector2 moveVector = new Vector2(moveVelocityX * moveDirection * moveSpeed * Time.deltaTime, moveVelocityY + _rigidbody.velocity.y);
            _rigidbody.velocity =  moveVector;
        }
        
        public void SetTarget(GameObject acquiredTarget) //Called from Sensor when target is acquired!
        {
            target = acquiredTarget;
        }

        public void HandleClearTarget()
        {
            StartCoroutine(ClearTarget());
        }
        
        IEnumerator ClearTarget()
        {
            Debug.Log("Clearing Target");
            yield return new WaitWhile(() => hasTarget == false);
            yield return new WaitForSeconds(10);
            SetTarget(null);
        }

        private void HandleGravity()
        {
            if (Sensor.isGrounded && !isFalling)
            {
                currentGravity = constantGravity;
            }
            else
            {
                if (currentGravity > maxGravity)
                {
                    currentGravity += -gravity * gravityScale * Time.deltaTime;
                    
                }
            }
            moveVelocityY = currentGravity * Time.deltaTime;
  
        }

        private void HandleSlopes()
        {
            if (isSloping)
            {
                var slopeSpeed = 1.1f;
                moveVelocityY += Sensor.upVelocityWhenSloping;
            }
        }

        public void TransitionToState(IEnemyState state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }

        public void HandleFlip()
        {
            StartCoroutine(Flip());
        }

        IEnumerator Flip()
        {
            _isFlipped = true;
            yield return new WaitForEndOfFrame();
            moveDirection = -moveDirection;
            Vector3 flipScale = transform.localScale;
            flipScale.x *= -1;
            transform.localScale = flipScale;
            _isFlipped = false;

        }

        public void Jump(float newJumpForce)
        {
            _rigidbody.AddForce(Vector2.up * newJumpForce, ForceMode2D.Impulse);
        }
    }
}
