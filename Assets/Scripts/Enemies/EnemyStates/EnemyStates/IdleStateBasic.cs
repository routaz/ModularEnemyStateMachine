using System;
using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    public class IdleStateBasic : IdleState
    {
        public IdleStateBasic(string newHelloText, float newTimeInIdle, bool newChangeStateIfDetectedPlayer)
        {
            helloText                   = newHelloText;
            timeInIdle                  = newTimeInIdle;
            changeStateIfDetectedPlayer = newChangeStateIfDetectedPlayer;
        }

        private EnemyBaseStateMachine   _machine;
        
        private readonly string         helloText;
        private readonly float          timeInIdle;
        private readonly bool           changeStateIfDetectedPlayer;
        
        public override void EnterState(EnemyBaseStateMachine machine)
        {
            if (helloText != String.Empty)
            {
                Debug.Log(helloText);
            }
            _machine = machine;
            _machine.StartCoroutine(HandleWaitInIdleState());
        }

        public override void Update()
        {
            //throw new System.NotImplementedException();
        }

        public override void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public override void TransitionToState(EnemyBaseState state)
        {
            _machine.TransitionToState(state);
        }
        
        IEnumerator HandleWaitInIdleState()
        {
            if (!changeStateIfDetectedPlayer)
            {
                yield return new WaitForSeconds(timeInIdle);
                TransitionToState(_machine.patrolState);
            }
            else
            {
                yield return new WaitWhile(() => _machine.target == null);
                Debug.Log("I SAW TARGET");
                TransitionToState((_machine.patrolState));
            }
            
        }
    }
}
