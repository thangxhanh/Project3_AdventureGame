using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeAlive;

    private float timeStart;
    private bool isFire = false;

    private Vector3 direction;
    public void Setup(Vector3 direction)
    {
        this.direction = direction;
        timeStart = Time.time;
        isFire = true;
    }

    private void Update()
    {
        if (!isFire) return;
        
        if (Time.time - timeStart > timeAlive) {
            Destroy(gameObject);
        } else {
            Move();
        }
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
