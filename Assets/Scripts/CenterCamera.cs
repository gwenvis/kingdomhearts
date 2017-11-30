using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

/// Centers the camera between the two transform and stays a certain distance behind target 1.
public class CenterCamera : MonoBehaviour {

    [SerializeField] private float minDistance = 2;
	[SerializeField] private float maxDistance = 10;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
	[SerializeField] private float horizontalBounds = 0.2f;


	[Header("Speed")] 
	[SerializeField] private float _cameraSpeed = 5; // set to player speed later.
	
	void Start () 
	{
		if(!target1 || !target2) Destroy(this); // terminate this script if there are not two targets.
	}
	
	void LateUpdate ()
	{
		var lookAtPoint = target1.position + target2.position;
		lookAtPoint.x /= 2;
		lookAtPoint.y /= 2;
		lookAtPoint.z /= 2;

		Vector3 velocity = new Vector3();
		
		// horizontal movement
		Vector2 screen = Camera.main.WorldToScreenPoint(target1.position);
		float w = screen.x / Screen.width;
		Debug.Log(screen.x);
		if (w < horizontalBounds) velocity.x = -_cameraSpeed;
		else if (w > 1 - horizontalBounds) velocity.x = _cameraSpeed;

		// vertical movement
		Vector3 vectorToPlayer = target1.position - transform.position;// - target1.position;
		float distance = Vector3.Project(vectorToPlayer, transform.forward).magnitude;
		if (distance > maxDistance) velocity.z = _cameraSpeed;
		else if (distance < minDistance) velocity.z = -_cameraSpeed;

		transform.Translate(velocity * Time.deltaTime, Space.Self);
		transform.LookAt(lookAtPoint);

	}
}
