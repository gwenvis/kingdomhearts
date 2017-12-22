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
	[SerializeField] private PlayerAnimation _plyAnim;
	
	// Use this for initialization
	void Start () {
		hit = UnityEngine.Resources.Load<GameObject>("HitBall");
		if (!_plyAnim) Debug.LogError("SwordCollisionHandler :: Fuck u bitch.");
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Enemy"
		    || !_plyAnim.AnimController.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) return;
		
		GameObject particle = Instantiate(hit);
		particle.transform.position = spawnPoint.transform.position;
		Destroy(particle, 2);
		
		EnemyAI eai = other.gameObject.GetComponent<EnemyAI>();
		
		Debug.Log("SwordCollisionHandler :: Trigger Activated Tag:" + other.gameObject.tag);
		
		if (eai) {
			Debug.Log("SwordCollisionHandler :: Enemy State Switched");
			if(eai.CurrentState.GetType() != typeof(HitState))
				eai.CurrentState = new HitState();
			
		}
		
		Target.instance.ShowAttack();
	}
}
