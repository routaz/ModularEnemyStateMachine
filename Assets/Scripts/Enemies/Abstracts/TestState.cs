using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    public class TestState : PatrolState
    {
        public TestState(string newHelloText, float newJumpForce, float newMoveSpeed, float newDurationBetweenJumps, float newDelayBeforeJumpingStarts)
        {
            helloText = newHelloText;
            jumpForce = newJumpForce;
            moveSpeed = newMoveSpeed;
            durationBetweenJumps = newDurationBetweenJumps;
            delayBeforeJumpingStarts = newDelayBeforeJumpingStarts;
        }

        private float moveSpeed;
        private string helloText;
        private float jumpForce;
        private float durationBetweenJumps;
        private float delayBeforeJumpingStarts;
        
        private bool isPatrolling;
        private bool isJumping;

        private EnemyBaseStateMachine _machine;

        public override void EnterState(EnemyBaseStateMachine machine)
        {
            _machine = machine;
            Debug.Log(helloText);
            _machine.StartCoroutine(StartJumping());
            _machine.MoveSpeed = moveSpeed;
        }

        public override void FixedUpdate()
        {
            if (isPatrolling && _machine.Sensor.GroundCheck())
            {
                _machine.Jump(jumpForce);
                _machine.StartCoroutine(HandleJumpWait());
            }
        }

        IEnumerator StartJumping()
        {
            yield return new WaitForSeconds(delayBeforeJumpingStarts);
            _machine.StartCoroutine(HandleJumpWait());
        }

        IEnumerator HandleJumpWait()
        {
            isPatrolling = false;
            yield return new WaitUntil(() => _machine.Sensor.GroundCheck());
            yield return new WaitForSeconds(durationBetweenJumps);
            isPatrolling = true;
        }

        public override void TransitionToState(EnemyBaseState state)
        {
            _machine.TransitionToState(state);
        }

        public override void Update()
        {
            /*
            var sensor = _stateMachine.Sensor;
            if (sensor.TargetCheck())
            {
                TransitionToState(_stateMachine.attackState);
            }
            */
        }
    }
}
