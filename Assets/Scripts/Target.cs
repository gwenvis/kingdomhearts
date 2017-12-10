using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour {

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject player;

    [SerializeField] private Sprite idle;
    [SerializeField] private Sprite attackRange;
    [SerializeField] private Sprite hit;

    [SerializeField] private Image image;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Vector3.Distance(target.transform.position, player.transform.position) < 3f) {
	        image.sprite = attackRange;
	    }
	    else {
	        image.sprite = idle;
	    }

	    Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position = pos;
	}
}
