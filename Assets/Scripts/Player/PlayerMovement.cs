// Created by Timo Heijne
// camera relative move by Antonio Bottelier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	// Allen door Timo behalve
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _movementSpeed; // Deze, door Antonio.
    [SerializeField] private PlayerAnimation _playerAnimation;
	[SerializeField] private ParticleSystem _particleSystem;

	public bool CanMove { get; private set; }

	// Door Timo. Behalve de boolean init
	void Start () {
	    if (_rb == null)
	        Debug.LogError("PlayerMovement :: Rigidbody not found on player");
        if (_playerAnimation == null)
            Debug.LogError("PlayerMovement :: _playerAnimation not found");		
		if(_particleSystem == null)
			Debug.LogError("PlayerMovement :: _particleSystem not found");
		
		CanMove = true;
	}
	
	// (Deze dus door Antonio)
	void Update ()
	{
		if (!CanMove)
		{
			_playerAnimation.AnimController.SetBool("Walking", false);
			_rb.velocity = new Vector3(0, _rb.velocity.y, 0);
			return;
		}
		
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
		
		// Get left vector of character
		var lockon = TargetLock.Targets[1];
		var vector = lockon.transform.position - transform.position;
		var cross = Vector3.Cross(vector, Vector3.up);

		cross.y = vector.y = 0;
		
		Vector3 movement = vector.normalized * moveVertical + -cross.normalized * moveHorizontal;
        movement = movement.normalized * _movementSpeed;
		_rb.velocity = new Vector3(movement.x, _rb.velocity.y, movement.z);
		
        if (_rb.velocity.x == 0 && _rb.velocity.z == 0) {
            _playerAnimation.ChangePlayerStatus(PlayerAnimation.Status.Idle);
	        _particleSystem.Stop();
        } else
        {
	        _particleSystem.Play();
            _playerAnimation.ChangePlayerStatus(PlayerAnimation.Status.Walking);
	        var lookrot = _rb.velocity;
	        lookrot.y = 0;
	        lookrot.Normalize();
            transform.rotation = Quaternion.Slerp(transform.rotation, 
	            Quaternion.LookRotation(lookrot, Vector3.up), Time.deltaTime * 10);
        }          
    }

	// Antonio
	public void ToggleCanMove(bool canmove)
	{
		CanMove = canmove;
	}
}
