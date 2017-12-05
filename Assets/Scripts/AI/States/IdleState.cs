using System;

namespace AI
{
    public struct IdleState : State
    {
        public void Act(EnemyAI ai)
        {
            float distance = ai.GetTargetDistance();
            if (distance < ai.throwDistance)
            {
                int rand = UnityEngine.Random.Range(0, 20);
                State state;
                ai.CurrentState = rand < 0 ?  new AttackState() as State : new MoveState();
            }
            //else ai.CurrentState = new ThrowState();
        }
    }
}