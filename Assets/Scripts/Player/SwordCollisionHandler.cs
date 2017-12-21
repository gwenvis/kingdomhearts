// Created by Timo Heijne

using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class SwordCollisionHandler : MonoBehaviour
{
	private GameObject hit;
	[SerializeField] private GameObject spawnPoint;
	
	// Use this for initialization
	void Start () {
		hit = UnityEngine.Resources.Load<GameObject>("HitBall");
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Enemy") return;
		
		GameObject particle = Instantiate(hit);
		particle.transform.position = spawnPoint.transform.position;
		Destroy(particle, 2);
		
		EnemyAI eai = other.gameObject.GetComponent<EnemyAI>();
		
		Debug.Log("SwordCollisionHandler :: Trigger Activated Tag:" + other.gameObject.tag);
		
		if (eai) {
			Debug.Log("SwordCollisionHandler :: Enemy State Switched");
			eai.CurrentState = new HitState();
			
		}
		
		Target.instance.ShowAttack();
	}
}
