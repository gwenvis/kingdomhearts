// Created by Antonio Bottelier

using System;
using UnityEngine;

namespace AI
{
    public struct IdleState : State
    {
        public void Init(EnemyAI ai) {
            ai.GetAIAnimator().SetState(EnemyAnimation.State.idle);
        }

        public void Act(EnemyAI ai)
        {    
            Debug.Log("Test");
            float distance = ai.GetTargetDistance();
            if (distance < ai.throwDistance)
            {
                int rand = UnityEngine.Random.Range(0, 20);
                State state;
                ai.CurrentState = rand < 0 ?  new AttackState() as State : new MoveState();
            }
            else ai.CurrentState = new ThrowState();
        }
    }
}