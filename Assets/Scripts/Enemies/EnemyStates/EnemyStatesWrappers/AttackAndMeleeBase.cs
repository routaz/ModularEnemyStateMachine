using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    [CreateAssetMenu(menuName = "EnemyStates/AttackStates/AttackAndMelee")]
    public class AttackAndMeleeBase : EnemyAttackStateWrapper
    {
        private EnemyBaseStateMachine   _machine;

        [SerializeField] private string helloText;
        [SerializeField] private float  chargeSpeed;
        [SerializeField] private float  waitBeforeMeleeAttack;
        [SerializeField] private float  waitAfterMeleeAttack;
        [SerializeField] private float  distanceWhereMeleeOccurs;

        public override void Initialize(EnemyBaseStateMachine machine)
        {
            _machine = machine;
            _machine.attackState = new AttackAndMelee(helloText, chargeSpeed, waitBeforeMeleeAttack, waitAfterMeleeAttack, distanceWhereMeleeOccurs);
        }
    }
}
