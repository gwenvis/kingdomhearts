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
            ai.enemyAnimation.SetState(EnemyAnimation.State.thow);
            
            Debug.Log("ThrowState :: Init");
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