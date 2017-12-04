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
		
		// Get left vector of character
		var lockon = CenterCamera.Targets[1];
		var vector = lockon.transform.position - transform.position;
		var cross = Vector3.Cross(vector, Vector3.up);

		cross.y = vector.y = 0;
		
		Vector3 movement = vector.normalized * moveVertical + -cross.normalized * moveHorizontal;
        _rb.velocity = movement * _movementSpeed;

        if (_rb.velocity.x == 0 && _rb.velocity.z == 0) {
            _playerAnimation.ChangePlayerStatus(PlayerAnimation.Status.Idle);
        } else {
            _playerAnimation.ChangePlayerStatus(PlayerAnimation.Status.Walking);
            transform.rotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
        }          
    }
}
