using System;
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
	[SerializeField] private float maxHOffset = 3;
	[SerializeField] private bool lerp = false;
	[SerializeField] private float camSpeed = 10;
	
	private float distance;
	private float hOffset;
	private float distHOffset;
	private Vector3 wantedPosition;
	private Vector3 staticOffset;
	private Vector3 lastT1Pos;
	
	void Start () 
	{
		if(!target1 || !target2) Destroy(this); // terminate this script if there are not two targets.
		staticOffset = new Vector3(0, 2f, 0);
		distance = 4;
		hOffset = -1f;
		distHOffset = hOffset;
		wantedPosition = target1.position;
		lastT1Pos = target1.position;
	}
	
	void LateUpdate ()
	{
		var lookAtPoint = target1.position + target2.position;
		lookAtPoint.x /= 2;
		lookAtPoint.y /= 2;
		lookAtPoint.z /= 2;

		var vec = (target1.position - lastT1Pos);
		var mag = vec.magnitude;
		
		var dist = Vector3.Project(vec, transform.right);
		var sign = Mathf.Sign(transform.InverseTransformVector(dist).x);
		hOffset += dist.magnitude * sign;
		
		var targetvector = target2.position - target1.position;
		var targetvectornormal = targetvector.normalized;
		var cross = Vector3.Cross(targetvectornormal, Vector3.up);
		
		SetDist(targetvectornormal);
		
		if (Mathf.Abs(hOffset) > maxHOffset && mag > 0.001f)
		{
			hOffset = maxHOffset * Mathf.Sign(hOffset);
			distHOffset = hOffset;
		}

		SetHorizontalPos(cross, hOffset);
		
		transform.position = lerp ? 
			Vector3.Lerp(transform.position, wantedPosition, 
				Time.deltaTime * camSpeed) : wantedPosition;
		
		transform.LookAt(lookAtPoint);
		lastT1Pos = target1.position;
	}

	void SetDist(Vector3 targetvectornormal)
	{
		wantedPosition = target1.position + (-targetvectornormal * distance) + staticOffset;
	}

	void SetHorizontalPos(Vector3 cross, float h)
	{
		wantedPosition += cross.normalized * h;
	}
	
}
