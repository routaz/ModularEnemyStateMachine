using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal.Enemies
{
    public abstract class EnemyBaseState : IEnemyState
    {
        public abstract void EnterState(EnemyBaseStateMachine machine);

        public abstract void Update();

        public abstract void TransitionToState(EnemyBaseState state);

        public abstract void FixedUpdate();
    }
}
