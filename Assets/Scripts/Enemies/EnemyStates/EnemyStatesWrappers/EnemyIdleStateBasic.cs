using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    [CreateAssetMenu(menuName = "EnemyStates/IdleStates/EnemyIdleStateBasic")]
    public class EnemyIdleStateBasic : EnemyIdleStatesWrapper
    {
        EnemyBaseStateMachine           _stateMachine;
        
        [SerializeField] private string helloText;
        [SerializeField] private float  timeInIdle;
        [SerializeField] private bool   doesAttackOnSight;
            
        public override void Initialize (EnemyBaseStateMachine machine)
        {
            _stateMachine = machine;
            _stateMachine.idleState = new IdleStateBasic(helloText, timeInIdle,doesAttackOnSight);
        }
        
    }
}
