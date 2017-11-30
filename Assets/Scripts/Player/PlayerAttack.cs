using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerAttack : MonoBehaviour {

    [SerializeField] private PlayerAnimation _playerAnimation;

	// Use this for initialization
	void Start () {
		if(_playerAnimation == null)
            Debug.LogError("PlayerAttack :: Player Animation Is Null");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
