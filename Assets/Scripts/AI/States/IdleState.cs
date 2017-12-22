// Created by Antonio Bottelier
// Init en de zooi voor de else statement van Timo

using System;
using System.Timers;
using UnityEngine;

namespace AI
{
    public struct IdleState : State {

        private float timer;
        
        public void Init(EnemyAI ai) {
            timer = ai.decisionTime;
            ai.enemyAnimation.SetState(EnemyAnimation.State.idle);
            ai.navAgent.isStopped = true;
            Debug.Log("IdleState :: Init");
        }

        public void Act(EnemyAI ai) {
            timer -= Time.deltaTime;

            if (timer <= 0) {                
                float distance = ai.GetTargetDistance();
                if (distance < ai.throwDistance) {
                    int rand = UnityEngine.Random.Range(0, 20);
                    State state;
                    ai.CurrentState = rand < 8 ? new ThrowState() as State : new MoveState();
                }
                else {
                    int rand = UnityEngine.Random.Range(0, 20);
                    Debug.Log(rand);
                    ai.CurrentState = rand < 5 ? new MoveState() as State : new ThrowState();
                }
            }
        }
    }
}