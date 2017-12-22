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
	private BoxCollider box;
	private PlayerSound _plySound;

	private bool hasHit = false;
	
	// Use this for initialization
	void Start () 
	{
		hit = UnityEngine.Resources.Load<GameObject>("HitBall");
		if (!_plyAnim) Debug.LogError("SwordCollisionHandler :: Fuck u bitch.");

		_plySound = _plyAnim.GetComponent<PlayerSound>();
		box = GetComponent<BoxCollider>();		
	}

	private void Update()
	{
		if (_plyAnim.AnimController.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !hasHit)
		{
			var cols = Physics.OverlapBox(transform.position + box.center, box.size, box.transform.rotation);
			
			foreach (var col in cols)
			{
				if (col.CompareTag("Enemy"))
					TriggerHit(col);

				StartCoroutine(ResetCollision());
			}
		}
	}

	IEnumerator ResetCollision()
	{
		while(_plyAnim.AnimController.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
		{yield return new WaitForEndOfFrame();}
		hasHit = false;
	}

	private void TriggerHit(Collider other) 
	{
		GameObject particle = Instantiate(hit);
		particle.transform.position = spawnPoint.transform.position;
		Destroy(particle, 2);
		
		EnemyAI eai = other.gameObject.GetComponent<EnemyAI>();
		
		Debug.Log("SwordCollisionHandler :: Trigger Activated Tag:" + other.gameObject.tag);
		
		if (eai) 
		{
			Debug.Log("SwordCollisionHandler :: Enemy State Switched");
			var type = eai.CurrentState.GetType();	// deze line door antonio
			if (type != typeof(HitState))
			{
				// deze ook
				eai.CurrentState = new HitState();
			}
			
			_plySound.PlayRandomCollisionSound();
			hasHit = true;
		}
		
		Target.instance.ShowAttack();
	}
}
