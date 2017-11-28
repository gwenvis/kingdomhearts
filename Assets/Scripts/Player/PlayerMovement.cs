// Created by Timo Heijne

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private PlayerAnimation _playerAnimation;

	// Use this for initialization
	void Start () {
	    if (_rb == null)
	        Debug.LogError("PlayerMovement :: Rigidbody not found on player");

        if (_playerAnimation == null)
            Debug.LogError("PlayerMovement :: _playerAnimation not found");
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.velocity = movement * _movementSpeed;

        if (_rb.velocity.x == 0 && _rb.velocity.z == 0) {
            _playerAnimation.ChangePlayerStatus(PlayerAnimation.Status.idle);
        } else {
            _playerAnimation.ChangePlayerStatus(PlayerAnimation.Status.walking);
            transform.rotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
        }          
    }
}
