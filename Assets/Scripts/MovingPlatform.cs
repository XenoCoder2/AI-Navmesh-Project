using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    private float waitTime = 2f;
    public int waypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) > 0.01f)
        {
            Vector3 directionToWaypoint = waypoints[waypointIndex].position - transform.position;
            directionToWaypoint.Normalize();
            transform.position += 2 * Time.deltaTime * directionToWaypoint;
        }
        else
        {
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                if (waypointIndex == waypoints.Length - 1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex++;
                }

                waitTime = 2f;
            }
        }
    }
}
