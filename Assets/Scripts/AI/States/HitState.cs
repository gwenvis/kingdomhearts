// A magnificently modified product of MoveState...

using UnityEngine;

namespace AI
{
    public struct HitState : State
    {
        private Vector3 direction;

        public void Init(EnemyAI ai) {
            ai.enemyAnimation.SetState(EnemyAnimation.State.hit);
            
            direction = ai.GetTargetVector();
            direction.y = 0;
            direction.Normalize();

            ai.navAgent.isStopped = true;
            ai.navAgent.velocity = direction.normalized * 5;

            Debug.Log("HitState :: Init");
        }

        public void Act(EnemyAI ai)
        {            
            if (ai.GetTargetDistance() * 2 > ai.moveDistance)
                ai.CurrentState = new IdleState();
        }
    }
}