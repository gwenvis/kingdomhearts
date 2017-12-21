using UnityEngine;

namespace AI
{
    public struct ThrowState : State
    {
        
        private ThrowStage stage;
        private float time;

        public void Init(EnemyAI ai) {
            stage = ThrowStage.Preparing;
            time = UnityEngine.Time.time;
            ai.GetAIAnimator().SetState(EnemyAnimation.State.thow);
        }

        public void Act(EnemyAI ai)
        {
            var dir = ai.GetTargetVector();
            dir.y = 0;
            var o = dir.normalized;
            
            if (stage == ThrowStage.Preparing)
            {
                ai.transform.rotation = Quaternion.LookRotation(-o, Vector3.up);
            }
            
            /*if (stage == ThrowStage.Preparing)
            {
                ai.transform.rotation = Quaternion.LookRotation(-o, Vector3.up);

                if (UnityEngine.Time.time > time + 2)
                {
                    time = UnityEngine.Time.time;
                    stage = ThrowStage.Throwing;


                    var ob = UnityEngine.Object
                        .Instantiate(ai.throwingBall, ai.transform.position, ai.transform.rotation)
                        .GetComponent<AIBall>();

                    var vec = ai.GetTargetVector();
                    
                    ob.SetValues(ai.transform.position, -vec, ai.GetTargetDistance(), -vec.y);
                }

            }
            else if (UnityEngine.Time.time > time + 1f && stage == ThrowStage.Throwing)
            {
                ai.CurrentState = new IdleState();
            }*/

            if (stage == ThrowStage.Throwing) {
                ai.CurrentState = new IdleState();
            }
        }

        public void SpawnBall(EnemyAI ai) {
            stage = ThrowStage.Throwing;

            Vector3 ballPos = ai.transform.Find("ball").transform.position;

            var ob = UnityEngine.Object
                .Instantiate(ai.throwingBall, ballPos, ai.transform.rotation)
                .GetComponent<AIBall>();

            var vec = ai.GetTargetVector();
                    
            ob.SetValues(ballPos, -vec, ai.GetTargetDistance(), -vec.y);
            
            ai.CurrentState = new IdleState();
        }
        

        enum ThrowStage
        {
            Preparing,
            Throwing
        }
    }
}