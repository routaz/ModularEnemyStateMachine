using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal.Enemies
{
    public interface IEnemyState
    {
        public abstract void EnterState(EnemyBaseStateMachine machine);
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void TransitionToState(EnemyBaseState state);
      
    }
}
