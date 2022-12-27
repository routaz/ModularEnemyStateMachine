using System.Collections;
using System.Collections.Generic;
using Bengal.Enemies;
using UnityEngine;

namespace Bengal
{
    public abstract class EnemyStatesWrapper : ScriptableObject
    {
        public abstract void Initialize(EnemyBaseStateMachine machine);

    }
}
