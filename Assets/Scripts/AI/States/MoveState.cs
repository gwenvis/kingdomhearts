using UnityEngine;

namespace AI
{
    public struct MoveState : State
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
            if (ai.GetTargetDistance() > ai.throwDistance)
                ai.CurrentState = new IdleState();
        }
    }
}