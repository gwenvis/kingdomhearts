using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestinCameraPlacement : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemy;

    private void LateUpdate() {
        transform.rotation.SetLookRotation(enemy.transform.position);

        Debug.DrawLine(player.transform.position, enemy.transform.position, Color.red);
        Debug.DrawLine(player.transform.position, transform.position, Color.black);
        Debug.DrawLine(enemy.transform.position, transform.position, Color.black);

        Vector3 centerPoint = (player.transform.position + enemy.transform.position) / 2;
        Vector3 cPointUp = centerPoint;
        cPointUp.y += 3;
        Debug.DrawLine(centerPoint, cPointUp, Color.blue);



        transform.LookAt(centerPoint);
    }
}
