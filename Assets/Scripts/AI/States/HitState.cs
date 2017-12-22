// A magnificently modified product of MoveState...
// Door Timo

using UnityEngine;

namespace AI
{
    public struct HitState : State
    {
        private Vector3 direction;
        private Vector3 velocity; // antonio

        public void Init(EnemyAI ai) {
            ai.enemyAnimation.SetState(EnemyAnimation.State.hit);
            
            direction = ai.GetTargetVector();
            direction.y = 0;
            direction.Normalize();
            
            velocity = direction * 10; // antonio

            ai.navAgent.isStopped = true;
            ai.Hit();
            
            Debug.Log("HitState :: Init");
        }

        public void Act(EnemyAI ai)
        {
            ai.navAgent.Move(velocity * Time.deltaTime); // antonio

            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * 5);
            
            if (velocity.magnitude < 0.2f) // antonio + volgende line
                ai.CurrentState = new IdleState();
        }
    }
}