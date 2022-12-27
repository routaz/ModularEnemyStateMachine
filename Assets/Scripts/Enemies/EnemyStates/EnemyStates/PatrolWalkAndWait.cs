using System;
using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Bengal.Enemies
{
    public class PatrolWalkAndWait : PatrolState
    {
        public PatrolWalkAndWait(string newHelloText, float newPatrolWalkSpeed, float newDurationOfTheWaiting, bool newAttacksOnSight)
        {
            helloText            = newHelloText;
            patrolWalkSpeed      = newPatrolWalkSpeed;
            durationOfTheWaiting = newDurationOfTheWaiting;
            attacksOnSight       = newAttacksOnSight;
        }

        EnemyBaseStateMachine   _machine;
        
        private readonly string helloText;
        private readonly float  patrolWalkSpeed;
        private readonly float  durationOfTheWaiting;
        
        private readonly bool   attacksOnSight;
        private bool            isWaiting;
        private bool            hasHitWallOrEdge;
        private bool            hasTarget;
        
        public override void EnterState(EnemyBaseStateMachine machine)
        {
            if (helloText != String.Empty)
            {
                Debug.Log(helloText);
            }
            _machine = machine;
            _machine.MoveVelocityX = 1;
            _machine.MoveSpeed = patrolWalkSpeed;
        }

        public override void FixedUpdate()
        {

        }

        public override void TransitionToState(EnemyBaseState state)
        {
            _machine.TransitionToState(state);
        }

        public override void Update()
        {
            hasTarget = _machine.HasTarget;
            hasHitWallOrEdge = !_machine.Sensor.GroundEndCheck() || _machine.Sensor.WallCheck();
            
            if(hasHitWallOrEdge && !isWaiting)
            {
                _machine.StartCoroutine(HandleWaiting());
            }

            if (attacksOnSight && hasTarget)
            {
                TransitionToState(_machine.attackState);
            }
        }
        
        IEnumerator HandleWaiting()
        {
            isWaiting = true;
            _machine.MoveVelocityX = 0;
            yield return new WaitForSeconds(durationOfTheWaiting);
            _machine.MoveVelocityX = 1;
            isWaiting = false;
        }
    }
}
