// Created by Antonio Bottelier

using UnityEngine;

namespace AI
{
    public struct AttackState : State
    {
        public void Init(EnemyAI ai) {
            ai.GetAIAnimator().SetState(EnemyAnimation.State.attack);
        }

        public void Act(EnemyAI ai)
        {
            
        }
    }
}