using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBall : MonoBehaviour
{
    private float speed = 15;
    private float height = 2;
    private float x, a, c;
    private float distance;

    private Vector3 origin, direction;

    public void SetValues(Vector3 origin, Vector3 direction, float distance)
    {
        this.distance = distance;
        this.origin = origin;
        this.direction = direction;
        this.direction.y = 0;
        this.direction.Normalize();
        x = 0;

        c = distance / 2;
        a = height / (c * c);
    }

    private void Update()
    {
        // y = -(a(x-c)^2) + b
        float y = -(a*((x-c)*(x-c))) + height;

        Debug.Log(y);

        var pos = direction * x;
        pos.y = y;
        transform.position = origin + pos;
        
        x += Time.deltaTime * speed;

    }
}