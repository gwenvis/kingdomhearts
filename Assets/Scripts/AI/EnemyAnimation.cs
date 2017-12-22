// Created by Timo Heijne

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour {

	private Animator animController;

	private State enemyAnimState = State.idle;

	public enum State {
		idle = 5,
		walking = 1,
		@throw = 2,
		hit = 3,
		attack = 4
	}
	
	// Use this for initialization
	void Start () {
		animController = GetComponent<Animator>();
	}

	public void SetState(State state) {
		switch(state) {
			case State.idle:
				animController.SetBool("Walking", false);
				break;
			case State.walking:
				animController.SetBool("Walking", true);
				break;
			case State.@throw:
				animController.SetBool("Walking", false);
				animController.SetTrigger("Throw");
				break;
			case State.hit:
				animController.SetBool("Walking", false);
				animController.SetTrigger("Hit");
				break;
			case State.attack:
				animController.SetBool("Walking", false);
				throw new NotImplementedException();
				break;
			default:
				throw new ArgumentOutOfRangeException("state", state, null);
		}
	}
	
	public Animator GetController() {
		return animController;
	}
}
