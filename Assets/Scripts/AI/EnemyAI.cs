// Created by Antonio Bottelier
// Enemy Animation implemented by Timo Heijne

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace AI
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyAI : MonoBehaviour
    {
        public GameObject throwingBall;
        public float LastThrowTime { get; private set; }
        private State _currentState = new IdleState();
        public readonly float throwDistance = 3;
        public readonly float moveDistance = 6;
        public readonly float moveSpeed = 12f;

        private EnemyAnimation enemyAnimation;
        
        public Rigidbody RgdBody { get; private set; }
        public GameObject Target { get; private set; }
        
        public State CurrentState {
            get { return _currentState; }
            set { _currentState = value; _currentState.Init(this); }
        } 

        private void Start()
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            
            enemyAnimation = GetComponent<EnemyAnimation>();
            
            RgdBody = GetComponent<Rigidbody>();
            if(!throwingBall)
                throwingBall = UnityEngine.Resources.Load<GameObject>("StrandBall");
            
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

        public EnemyAnimation GetAIAnimator() {
            return enemyAnimation;
        }
    }
}