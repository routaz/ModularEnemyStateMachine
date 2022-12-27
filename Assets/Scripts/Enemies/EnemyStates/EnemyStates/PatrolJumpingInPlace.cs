using System;
using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    public class PatrolJumpingInPlace : PatrolState
    {
        public PatrolJumpingInPlace(string newHelloText, float newJumpForce, float newDurationBetweenJumps, bool newAttacksOnSight)
        {
            helloText            = newHelloText;
            jumpForce            = newJumpForce;
            durationBetweenJumps = newDurationBetweenJumps;
            attacksOnSight       = newAttacksOnSight;
        }

        EnemyBaseStateMachine   _machine;
        
        private readonly string helloText;
        private readonly float  jumpForce;
        private readonly float  durationBetweenJumps;
        private readonly bool   attacksOnSight;
        
        private bool isPatrolling;
        private bool isJumping;
        
        public override void EnterState(EnemyBaseStateMachine machine)
        {
            if (helloText != String.Empty)
            {
                Debug.Log(helloText);
            }
            _machine = machine;
            isPatrolling = true;
        }

        public override void FixedUpdate()
        {
            if (isPatrolling && _machine.IsGrounded)
            {
                _machine.StartCoroutine(HandleJumpWait());
            }
        }

        IEnumerator HandleJumpWait()
        {
            isPatrolling = false;
            _machine.Jump(jumpForce);
            _machine.HandleFlip();
            yield return new WaitForSeconds(durationBetweenJumps);
            isPatrolling = true;
        }

        public override void TransitionToState(EnemyBaseState state)
        {
            _machine.TransitionToState(state);
        }

        public override void Update()
        {
            if (attacksOnSight && _machine.HasTarget)
            {
                TransitionToState(_machine.attackState);
            }

        }
    }
}
