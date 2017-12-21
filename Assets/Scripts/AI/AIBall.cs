using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBall : MonoBehaviour
{
    private float speed = 15;
    private float height = 2;
    private float x, a, c, o;
    private float distance, heightdifference;
    
    private Vector3 origin, direction;

    void Start() {
        Destroy(gameObject, 5);
    }

    // Maybe a minimum distance is needed? maybe.
    public void SetValues(Vector3 origin, Vector3 direction, float distance, float heightdifference)
    {
        this.origin = origin;
        this.direction = direction;
        this.direction.y = 0;
        this.direction.Normalize();
        this.distance = distance;
        //Debug.Log(distance);
        if (this.distance < 6) this.distance = 6;
        this.heightdifference = heightdifference;
        
        x = 0;
        c = this.distance / 2;
        a = height / (c * c);
    }

    private void Update()
    {
        // y = -(ax^2) + b
        float y = -(a*(x-c)*(x-c)) + height;
        o = x / distance * heightdifference;

        var pos = direction * x;
        pos.y = y + o;
        transform.position = origin + pos;
        
        x += Time.deltaTime * speed;

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}