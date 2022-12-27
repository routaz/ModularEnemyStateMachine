using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Bengal.Enemies;
using Unity.Mathematics;
using UnityEngine;

namespace Bengal
{
    public class AttackAndMelee : AttackState
    {
        public AttackAndMelee(string newHelloText, float newChargeSpeed, float newWaitBeforeMeleeAttack, float newWaitAfterMeleeAttack, float newDistanceWhereMeleeOccurs)
        {
            helloText                = newHelloText;
            chargeSpeed              = newChargeSpeed;
            waitBeforeMeleeAttack    = newWaitBeforeMeleeAttack;
            waitAfterMeleeAttack     = newWaitAfterMeleeAttack;
            distanceWhereMeleeOccurs = newDistanceWhereMeleeOccurs;
        }
        
        EnemyBaseStateMachine  _machine;
        
        private string         helloText;
        private readonly float chargeSpeed;
        private readonly float waitBeforeMeleeAttack;
        private readonly float waitAfterMeleeAttack;
        private readonly float distanceWhereMeleeOccurs;

        private Vector2        targetPos;
        private float          distance;
        private GameObject     target;
        private bool           hasTarget;
        private bool           isCharging;
        private bool           isFlipped;
        
        // Update is called once per frame
        public override void EnterState(EnemyBaseStateMachine machine)
        {
            if (helloText != String.Empty)
            {
                Debug.Log(helloText);
            }
            _machine = machine;
        }

        public override void Update()
        {
            hasTarget = _machine.target;
            if (hasTarget)
            {
                target = _machine.target;
            }
            else if(!hasTarget && !isCharging)
            {
                TransitionToState(_machine.patrolState);
            }
            
            targetPos = (target.transform.position - _machine.transform.position).normalized;
            distance = Vector2.Distance(target.transform.position, _machine.transform.position);
            
            if (distance < distanceWhereMeleeOccurs && !isCharging)
            {
                _machine.StartCoroutine(HandleMelee());
            }

            if (_machine.isStopped && _machine.MoveVelocityX != 0)
            {
                _machine.MoveVelocityX = 0;
            }
        }

        IEnumerator HandleMelee()
        {
            isCharging = true;
            _machine.MoveVelocityX = 0;
            yield return new WaitForSeconds(waitBeforeMeleeAttack);
            _machine.transform.GetChild(3).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _machine.transform.GetChild(3).gameObject.SetActive(false);
            yield return new WaitForSeconds(waitAfterMeleeAttack);
            isCharging = false;
        }

        public override void FixedUpdate()
        {
            if (!isCharging)
            {
                if((target.transform.position.x > _machine.transform.position.x) && !_machine.isFacingRight)
                {
                    _machine.HandleFlip();
                }
                if ((target.transform.position.x < _machine.transform.position.x) && _machine.isFacingRight)
                {
                    _machine.HandleFlip();
                }

                if (!_machine.isStopped)
                {
                    _machine.MoveVelocityX = 1f;
                    _machine.MoveSpeed = chargeSpeed;
                }
            }
        }

        public override void TransitionToState(EnemyBaseState state)
        {
            _machine.TransitionToState(state);
        }
    }
}
