using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    #region Variables
    //The waypoints the platform will move between.
    public Transform[] waypoints;
    //The time the platform will wait at a waypoint.
    private float waitTime = 2f;
    [Header("Waypoint Index")]
    //Tracks the current waypoint the platform is moving towards.
    public int waypointIndex = 0;
    #endregion

    #region Update Method 
    // Update will move the platform through different points and evaluate whether it should wait or continue moving.
    void Update()
    {
        //If the distance between the transform.position and the currently searched waypoint is greater than 0.01f.
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) > 0.01f)
        {
            //Create a Vector3 directionToWaypoint and set it to the currently searched waypoint minus the platform's transform.position.
            Vector3 directionToWaypoint = waypoints[waypointIndex].position - transform.position;
            //Normalise the directionToWaypoint variable.
            directionToWaypoint.Normalize();
            //Apply the values to the transform.position of the platform.
            transform.position += 2 * Time.deltaTime * directionToWaypoint;
        }
        //Else the platform is stopped.
        else
        {
            //Count down from waitTime.
            waitTime -= Time.deltaTime;

            //If waitTime is less than or equal to 0.
            if (waitTime <= 0)
            {
                //If the waypointIndex is equal to the length of the waypoints array - 1.
                if (waypointIndex == waypoints.Length - 1)
                {
                    //Set waypointIndex to 0.
                    waypointIndex = 0;
                }
                //Else
                else
                {
                    //Increase waypointIndex by 1.
                    waypointIndex++;
                }

                //Reset waitTime to 2f.
                waitTime = 2f;
            }
        }
    }
    #endregion
}
