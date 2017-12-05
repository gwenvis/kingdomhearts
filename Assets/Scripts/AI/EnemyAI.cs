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
        [SerializeField] private GameObject throwingBall;
        public GameObject Target { get; private set; }
        public float LastThrowTime { get; private set; }
        private State _currentState = new IdleState();
        public readonly float throwDistance = 6;
        public readonly float moveSpeed = 5f;
        public Rigidbody RgdBody { get; private set; }
        
        public State CurrentState {
            get { return _currentState; }
            set { _currentState = value; }
        } 

        private void Start()
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            RgdBody = GetComponent<Rigidbody>();
            
            if (Target == null) Destroy(this);
        }

        private void Update()
        {
            _currentState.Act(this);
        }

        public float GetTargetDistance()
        {
            return GetTargetVector().magnitude;
        }

        public Vector3 GetTargetVector()
        {
            return (transform.position - Target.transform.position);
        }
    }
}