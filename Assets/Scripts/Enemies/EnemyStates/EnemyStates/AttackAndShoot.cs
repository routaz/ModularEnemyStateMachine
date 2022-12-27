using System;
using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Bengal.Enemies
{
    public class AttackAndShoot : AttackState
    {
        public AttackAndShoot(string newHelloText, GameObject newProjectile, float newProjectileSpeed, int newAmountsAmountOfShotsBeforeChangingState, float newPreDelayBeforeShots, float newDelayBetweenShots)
        {
            helloText                        = newHelloText;
            projectile                       = newProjectile;
            projectileSpeed                  = newProjectileSpeed;
            amountOfShotsBeforeChangingState = newAmountsAmountOfShotsBeforeChangingState;
            preDelayBeforeShots              = newPreDelayBeforeShots;
            delayBetweenShots                = newDelayBetweenShots;
        }
        
        private EnemyBaseStateMachine _machine;
        
        private string                helloText;
        private GameObject            projectile;
        private float                 projectileSpeed;
        private int                   amountOfShotsBeforeChangingState;
        private float                 preDelayBeforeShots;
        private float                 delayBetweenShots;
        
        private int                   shotsFired;
        private bool                  isShooting;
        private bool                  hasTarget;

        public override void EnterState(EnemyBaseStateMachine machine)
        {
            if (helloText != String.Empty)
            {
                Debug.Log(helloText);
            }

            _machine = machine;
            shotsFired = 0;
            _machine.MoveVelocityX = 0;
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
            
            if(shotsFired >= amountOfShotsBeforeChangingState)
            {
                TransitionToState(_machine.patrolState);
            }
            else if(!isShooting && hasTarget)
            {
                _machine.StartCoroutine(HandleShooting());
            }
            
            if (_machine.isStopped)
            {
                _machine.MoveVelocityX = 0;
            }
        }

        IEnumerator HandleShooting()
        {
            Debug.Log("Handling shooting now!");
            isShooting = true;
            yield return new WaitForSeconds(preDelayBeforeShots);
            while (shotsFired < amountOfShotsBeforeChangingState)
            {
                Debug.Log("I am shooting!");
                Shoot();
                yield return new WaitForSeconds(delayBetweenShots);
            }
            
            Debug.Log("I am done with shooting");
            isShooting = false;
        }

        private void Shoot()
        {
            GameObject ammo = Object.Instantiate(projectile,_machine.transform.position,Quaternion.identity);
            var transform = _machine.transform;
            ammo.GetComponent<Rigidbody2D>().velocity = transform.right * (transform.localScale.x * projectileSpeed);
            shotsFired++;
        }
    }
}
