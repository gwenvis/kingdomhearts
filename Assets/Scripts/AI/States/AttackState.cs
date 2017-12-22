// Created by Antonio Bottelier

using System;
using UnityEngine;

namespace AI
{
    public struct AttackState : State
    {
        public void Init(EnemyAI ai) {
            ai.enemyAnimation.SetState(EnemyAnimation.State.attack);
            
            Debug.Log("AttackState :: Init");
        }

        public void Act(EnemyAI ai)
        {
            ai.CurrentState = new IdleState();        // door timo
            throw new NotImplementedException();      // of niet?
                                                      // jawel.
        }
    }
}