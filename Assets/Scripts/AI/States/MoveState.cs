// Created by Antonio Bottelier

using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public struct MoveState : State
    {
        private Vector3 direction;

        public void Init(EnemyAI ai) {
            ai.enemyAnimation.SetState(EnemyAnimation.State.walking);
            
            direction = ai.GetTargetVector();
            direction.y = 0;
            direction.Normalize();
            
            Vector3 randomDirection = Random.insideUnitSphere * ai.walkDistance;
            randomDirection += ai.transform.position;
            
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, ai.walkDistance, 1);
            Vector3 finalPosition = hit.position;
            
            ai.navAgent.destination = finalPosition;
            ai.navAgent.isStopped = false;
            
            Debug.Log("MoveState :: Init");
        }

        public void Act(EnemyAI ai)
        {
            //ai.RgdBody.MovePosition(ai.transform.position + direction * ai.moveSpeed * Time.deltaTime);
            ai.GetWalKParticle().Play();
            
            if (ai.navAgent.remainingDistance <= 0.5f)
            {
                ai.navAgent.isStopped = true;
                ai.GetWalKParticle().Stop();
                ai.CurrentState = new IdleState();             
            }              
        }
    }
}