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
	    Vector3 pos = transform.position;
	    Vector3 rot = transform.rotation.eulerAngles;

	    if (Input.GetKeyDown(KeyCode.W)) {
            transform.rotation = Quaternion.Euler(0,0,0);
        }

	    if (Input.GetKeyDown(KeyCode.S)) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

	    if (Input.GetKeyDown(KeyCode.A)) {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }

	    if (Input.GetKeyDown(KeyCode.D)) {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if(InputManager.IsAnyDown(new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D }))
            pos += transform.forward * _movementSpeed * Time.deltaTime;

        _rb.MovePosition(pos);
	}
}
