using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

// DOOR ANTONIO HEHEHEHEE

public class CameraActivator : MonoBehaviour
{
	private TargetLock cameraActivator;
	[SerializeField] private float speed = 6;
	[SerializeField] private GameObject enemy;
	[SerializeField] private GameObject player;
	private Vector3 wantedPosition;
	private float time;
	
	void Awake()
	{
		cameraActivator = GetComponent<TargetLock>();
		cameraActivator.Init();
		wantedPosition = cameraActivator.GetWantedPosition();
		cameraActivator.enabled = false;
		time = Time.time;

		player.GetComponent<PlayerAttack>().enabled = false;
		player.GetComponent<PlayerMovement>().enabled = false;
		enemy.GetComponent<EnemyAI>().enabled = false;
	}

	void Update()
	{
		if (Time.time < time + 0.5f) return;
		
		transform.position = Vector3.Lerp(transform.position, wantedPosition, speed * Time.deltaTime);
		if (Vector3.Distance(transform.position, wantedPosition) < 0.2f)
		{
			transform.position = wantedPosition;
			cameraActivator.enabled = true;
			
			player.GetComponent<PlayerAttack>().enabled = true;
			player.GetComponent<PlayerMovement>().enabled = true;
			enemy.GetComponent<EnemyAI>().enabled = true;
			
			Destroy(this);
		}

		Vector3 target1, target2;
		target1 = TargetLock.Targets[0].position;
		target2 = TargetLock.Targets[1].position;
		var lookAtPoint = (target1 + target2) / 2;
		transform.LookAt(lookAtPoint);

	}
}
