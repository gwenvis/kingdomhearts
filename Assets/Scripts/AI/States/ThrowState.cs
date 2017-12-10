using UnityEngine;

namespace AI
{
    public struct ThrowState : State
    {
        private bool setup;
        private ThrowStage stage;
        private float time;
        
        public void Act(EnemyAI ai)
        {
            if (!setup)
            {
                stage = ThrowStage.Preparing;
                time = UnityEngine.Time.time;
                setup = true;
            }

            var dir = ai.GetTargetVector();
            dir.y = 0;
            var o = dir.normalized;
            
            if (stage == ThrowStage.Preparing)
            {
                ai.transform.rotation = Quaternion.LookRotation(-o, Vector3.up);

                if (UnityEngine.Time.time > time + 2)
                {
                    time = UnityEngine.Time.time;
                    stage = ThrowStage.Throwing;


                    var ob = UnityEngine.Object
                        .Instantiate(ai.throwingBall, ai.transform.position, ai.transform.rotation)
                        .GetComponent<AIBall>();

                    ob.SetValues(ai.transform.position, -ai.GetTargetVector(), ai.GetTargetDistance());
                }

            }
            else if (UnityEngine.Time.time > time + 1f && stage == ThrowStage.Throwing)
            {
                ai.CurrentState = new IdleState();
            }
        }

        enum ThrowStage
        {
            Preparing,
            Throwing
        }
    }
}