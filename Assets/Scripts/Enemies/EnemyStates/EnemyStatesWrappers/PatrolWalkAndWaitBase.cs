using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    [CreateAssetMenu(menuName = "EnemyStates/PatrolStates/PatrolWalkAndWait")]
    public class PatrolWalkAndWaitBase : EnemyPatrolStateWrapper
    {
        EnemyBaseStateMachine           _machine;

        [SerializeField] private string helloText;
        [SerializeField] private float  patrolWalkSpeed;
        [SerializeField] private float  durationOfWait;
        [SerializeField] private bool   attackOnSight;

        public override void Initialize(EnemyBaseStateMachine machine)
        {
            _machine = machine;
            _machine.patrolState = new PatrolWalkAndWait(helloText, patrolWalkSpeed, durationOfWait, attackOnSight);
        }
    }
}
