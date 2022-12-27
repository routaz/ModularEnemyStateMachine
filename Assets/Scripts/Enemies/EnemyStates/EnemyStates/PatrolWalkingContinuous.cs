using System;
using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal.Enemies
{
    public class PatrolWalkingContinuous : PatrolState
    {
        public PatrolWalkingContinuous(string newHelloText, float newPatrolWalkSpeed, bool newAttacksOnSight)
        {
            helloText       = newHelloText;
            patrolWalkSpeed = newPatrolWalkSpeed;
            attacksOnSight  = newAttacksOnSight;
        }

        EnemyBaseStateMachine          _machine;
        
        private readonly string        helloText;
        private readonly float         patrolWalkSpeed;
        
        private readonly bool          attacksOnSight;
        private bool                   isPatrolling;
        private bool                   hasTarget;
       
        public override void EnterState(EnemyBaseStateMachine machine)
        {
            if (helloText != String.Empty)
            {
                Debug.Log(helloText);
            }
            _machine = machine;
            _machine.StartCoroutine(HandlePatrol());
        }
        
        public override void TransitionToState(EnemyBaseState state)
        {
            _machine.TransitionToState(state);
        }

        public override void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public override void Update()
        {
            hasTarget = _machine.HasTarget;
        }

        IEnumerator HandlePatrol()
        {
            _machine.MoveVelocityX = 1;
            _machine.MoveSpeed = patrolWalkSpeed;
            
            if (attacksOnSight)
            {
                yield return new WaitUntil(() => hasTarget);
                isPatrolling = false;
                TransitionToState(_machine.attackState);
            }
        }

    }
}
