using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    private ParticleSystem particleSystem;

    [SerializeField] private float speed = 2f;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f) {
            currentWaypointIndex = (currentWaypointIndex < waypoints.Length - 1) ? ++currentWaypointIndex : 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed);
    }
    private void OnBecameVisible()
    {
        particleSystem.Play();
    }

    private void OnBecameInvisible()
    {
        particleSystem.Pause();
    }
}
