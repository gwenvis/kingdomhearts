// Created by Timo Heijne

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _movementSpeed;

	// Use this for initialization
	void Start () {
	    if (_rb == null) {
	        Debug.LogError("Rigidbody not found on player");
	    }
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.velocity = movement * _movementSpeed;

        transform.rotation = Quaternion.LookRotation(_rb.velocity, Vector3.up);
        Debug.Log(Quaternion.LookRotation(_rb.velocity, Vector3.up));
    }
}
