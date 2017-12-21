// A magnificently modified product of MoveState...

using UnityEngine;

namespace AI
{
    public struct HitState : State
    {
        private Vector3 direction;

        public void Init(EnemyAI ai) {
            ai.GetAIAnimator().SetState(EnemyAnimation.State.hit);
            
            direction = ai.GetTargetVector();
            direction.y = 0;
            direction.Normalize();
            
            Debug.Log("ThrowState :: Init");
        }

        public void Act(EnemyAI ai)
        {
            ai.RgdBody.MovePosition(ai.transform.position + direction * ai.moveSpeed * Time.deltaTime);
            if (ai.GetTargetDistance() * 1.5f > ai.moveDistance)
                ai.CurrentState = new IdleState();
        }
    }
}