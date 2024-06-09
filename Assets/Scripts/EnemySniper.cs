using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniper : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private bool isRight;
    [SerializeField] private float timeFire = 2f;
    [SerializeField] private Vector3 offset;
    private float lastTimeFire = 2f;
    void Start()
    {
        lastTimeFire = Time.time;
    }

    private void Fire()
    {
        var b = Instantiate(bullet);
        b.transform.position = transform.position + offset * (isRight ? 1 : -1);
        b.Setup((isRight ? 1 : -1) * transform.right.normalized);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTimeFire > timeFire) {
            Fire();
            lastTimeFire = Time.time;
        }
    }
}
