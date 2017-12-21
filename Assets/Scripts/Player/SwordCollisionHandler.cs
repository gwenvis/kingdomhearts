// Created by Timo Heijne

using System;
using System.Collections;
using System.Collections.Generic;
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
		Target.instance.ShowAttack();
	}
}
