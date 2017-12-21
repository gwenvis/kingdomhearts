// Created by Timo Heijne

using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class SwordCollisionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other) {
		throw new NotImplementedException();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Enemy") return;
		
		EnemyAI eai = other.gameObject.GetComponent<EnemyAI>();
		
		Debug.Log("SwordCollisionHandler :: Trigger Activated Tag:" + other.gameObject.tag);
		
		if (eai) {
			Debug.Log("SwordCollisionHandler :: Enemy State Switched");
			eai.CurrentState = new HitState();
		}
		
		Target.instance.ShowAttack();
	}
}
