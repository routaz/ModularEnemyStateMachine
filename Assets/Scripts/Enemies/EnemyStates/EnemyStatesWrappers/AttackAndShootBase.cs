using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    [CreateAssetMenu(menuName = "EnemyStates/AttackStates/AttackAndShoot")]
    public class AttackAndShootBase : EnemyAttackStateWrapper
    {
        private EnemyBaseStateMachine       _machine;
        
        [SerializeField] private string     helloText;
        [SerializeField] private GameObject projectile;
        [SerializeField] private float      projectileSpeed;
        [SerializeField] private int        amountOfShots;
        [SerializeField] private float      preDelayBeforeShots;
        [SerializeField] private float      delayBetweenShots;
        
        public override void Initialize(EnemyBaseStateMachine machine)
        {
            _machine = machine;
            _machine.attackState = new AttackAndShoot(helloText, projectile, projectileSpeed, amountOfShots, preDelayBeforeShots, delayBetweenShots);
        }
    }
}
