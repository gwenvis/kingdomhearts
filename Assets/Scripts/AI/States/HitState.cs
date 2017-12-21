// A magnificently modified product of MoveState...

using UnityEngine;

namespace AI
{
    public struct HitState : State
    {
        private bool setup;
        private Vector3 direction;
        
        public void Act(EnemyAI ai)
        {
            if (!setup)
            {
                direction = ai.GetTargetVector();
                direction.y = 0;
                direction.Normalize();
                setup = true;
            }

            ai.RgdBody.MovePosition(ai.transform.position + direction * ai.moveSpeed * Time.deltaTime);
            if (ai.GetTargetDistance() * 1.5f > ai.moveDistance)
                ai.CurrentState = new IdleState();
        }
    }
}