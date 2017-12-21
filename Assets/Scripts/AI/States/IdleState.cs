// Created by Antonio Bottelier

using System;
using System.Timers;
using UnityEngine;

namespace AI
{
    public struct IdleState : State {

        private float timer;
        
        public void Init(EnemyAI ai) {
            timer = ai.decisionTime;
            ai.GetAIAnimator().SetState(EnemyAnimation.State.idle);
            
            Debug.Log("IdleState :: Init");
        }

        public void Act(EnemyAI ai) {
            timer -= Time.deltaTime;

            if (timer <= 0) {                
                float distance = ai.GetTargetDistance();
                if (distance < ai.throwDistance) {
                    int rand = UnityEngine.Random.Range(0, 20);
                    State state;
                    ai.CurrentState = rand < 0 ? new AttackState() as State : new MoveState();
                }
                else {
                    int rand = UnityEngine.Random.Range(0, 20);
                    ai.CurrentState = rand < 15 ? new MoveState() as State : new ThrowState();
                }
            }
        }
    }
}