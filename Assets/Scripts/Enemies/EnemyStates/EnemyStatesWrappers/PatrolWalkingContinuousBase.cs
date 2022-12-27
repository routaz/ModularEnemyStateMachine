using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    [CreateAssetMenu(menuName = "EnemyStates/PatrolStates/PatrolWalkingContinuous")]
    public class PatrolWalkingContinuousBase : EnemyPatrolStateWrapper
    {
        EnemyBaseStateMachine           _machine;
        
        [SerializeField] private string helloText;
        [SerializeField] private float  patrolSpeed;
        [SerializeField] private bool   attacksOnSight;

        public override void Initialize(EnemyBaseStateMachine machine)
        {
            _machine = machine;
            _machine.patrolState = new PatrolWalkingContinuous(helloText, patrolSpeed, attacksOnSight);
        }
    }
}
