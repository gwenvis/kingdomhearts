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

            Debug.Log("HitState :: Init");
        }

        public void Act(EnemyAI ai)
        {
            ai.RgdBody.MovePosition(ai.transform.position + direction * (ai.moveSpeed * 2) * Time.deltaTime);
            if (ai.GetTargetDistance() * 2f > ai.moveDistance)
                ai.CurrentState = new IdleState();
        }
    }
}