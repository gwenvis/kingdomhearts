// Created by Antonio Bottelier
// Enemy Animation, Ball Throwing, Particles implemented by Timo Heijne
// hou je bek timo

using UnityEngine;
using AI;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyAI : MonoBehaviour
    {
        public GameObject throwingBall;
        public float LastThrowTime { get; private set; }
        public float walkDistance = 15;
        private State _currentState;
        public readonly float throwDistance = 3;
        public readonly float moveDistance = 6;
        public readonly float moveSpeed = 12f;
        public readonly float decisionTime = 0.2f; // Means every X seconds the enemy decides whether he should attack, move, throw
        private const int hitReset = 8;
        private ParticleSystem walkParticle;
        private EnemySounds enemySound;

        private int hitCounter;

        public EnemyAnimation enemyAnimation { get; private set; }
        public NavMeshAgent navAgent { get; private set; }
        
        public Rigidbody RgdBody { get; private set; }
        public GameObject Target { get; private set; }
        
        public State CurrentState {
            get { return _currentState; }
            set { _currentState = value; _currentState.Init(this); }
        } 

        private void Start()
        {    
            walkParticle = transform.Find("walk").GetComponent<ParticleSystem>();
            walkParticle.Stop();
            Target = GameObject.FindGameObjectWithTag("Player");
            navAgent = GetComponent<NavMeshAgent>();
            
            enemyAnimation = GetComponent<EnemyAnimation>();
            CurrentState = new ThrowState();
            
            RgdBody = GetComponent<Rigidbody>();
            if(!throwingBall)
                throwingBall = UnityEngine.Resources.Load<GameObject>("StrandBall");
            enemySound = GetComponent<EnemySounds>();
            
            if (Target == null) Destroy(this);
        }

        private void Update()
        {
            _currentState.Act(this);
            RgdBody.velocity = new Vector3(0, RgdBody.velocity.y, 0);
        }

        public float GetTargetDistance()
        {
            return GetTargetVector().magnitude;
        }

        public Vector3 GetTargetVector()
        {
            return (transform.position - Target.transform.position);
        }

        public ParticleSystem GetWalKParticle()
        {
            return walkParticle;
        }

        public void Hit()
        {
            hitCounter++;
            enemySound.PlayRandomHurtSound();
            if (hitCounter > hitReset)
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        public void ThrowBall() {
            if (_currentState.GetType().Name == "ThrowState") {
                ThrowState state = (ThrowState)_currentState;
                state.SpawnBall(this);
            }
        }
    }
}