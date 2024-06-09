using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Pathfinding;

public class BirdAI : MonoBehaviour
{
    public Transform target;
    public float speed = 100f;
    public float nextWaypointDistance = 3f;
    public Transform birdGFX;


    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    public AstarPath astarPath;
    public int width;
    public int height;


    // Start is called before the first frame update
    void Start()
    {
        GridGraph gridGraph = (GridGraph)astarPath.graphs[0];

        // Retrieve the width and height of the grid graph
        width = gridGraph.width;
        height = gridGraph.depth;

        UnityEngine.Debug.Log("Grid Graph Width: " + width);
        UnityEngine.Debug.Log("Grid Graph Height: " + height);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (seeker != null && target != null)
        {
            InvokeRepeating("UpdatePath", 0f, 0.5f);
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) 
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        } else
        {
            reachedEndofPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
