using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    [CreateAssetMenu(menuName = "EnemyStates/PatrolStates/PatrolJumpingInPlace")]
    public class PatrolJumpingInPlaceBase : EnemyPatrolStateWrapper
    {
        private EnemyBaseStateMachine   _machine;
        
        [SerializeField] private string helloText;
        [SerializeField] private float  jumpForce;
        [SerializeField] private float  durationBetweenJumps;
        [SerializeField] private bool   attacksOnSight;
        
        public override void Initialize(EnemyBaseStateMachine machine)
        {
            _machine = machine;
            _machine.patrolState = new PatrolJumpingInPlace(helloText, jumpForce, durationBetweenJumps, attacksOnSight);
        }
   
    }
}
