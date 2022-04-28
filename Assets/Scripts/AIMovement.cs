using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//A base class used by all agents and provides the basic functionality all of them need.
public class AIMovement : MonoBehaviour
{
    #region Variables
    [Header("Waypoint Value")]
    //Used to indicate which waypoint the agent should go towards.
    public int waypointValue;
    [Header("Collectables")]
    //A list of collectables.
    public List<GameObject> collectables;
    [Header("Door Keys")]
    //The keys for the doors.
    public List<GameObject> keys;
    [Header("Doors")]
    //The doors associated with the agent.
    public List<GameObject> doors;
    [Header("Waypoints")]
    //The waypoints to help navigate through the maze.
    public List<Transform> guides;
    [Header("Agent Animator")]
    //The animator for the agent.
    public Animator anim;
    [Header("Total Keys Collected")]
    //The total amount of keys collected.
    public int keysCollected;
    [Header("Agent Finished")]
    //A bool to check if the agent has finished the maze.
    public bool finishedMaze;
    //The NavMeshAgent.
    protected NavMeshAgent _navAgent;
    //The amount an agent needs to move before triggering the walking animation.
    protected float moveThreshold = 0.01f;
    #endregion

    #region Update Method
    //The update method will continuously update the animation parameters and the current area speed.
    protected virtual void Update()
    {
        //Run the UpdateAnim method.
        UpdateAnim();
        //Run the ChangeAreaSpeed method.
        ChangeAreaSpeed();
    }
    #endregion

    #region Animation Update Method
    //Update Anim
    // - Checks what is currently happening with the agent and applies different animations depending on the situation.
    // - If the agent has not finished the maze, it will check the magnitude velocity against moveThreshold and evaluate whether it should be 
    // displaying the walking animation or not.
    // - Else if the maze has been finished, play the finished animation.
    public void UpdateAnim()
    {
        //If the maze is not yet finished.
        if (!finishedMaze)
        {
            //If the current magnitude is less than moveThreshold.
            if (_navAgent.velocity.magnitude < moveThreshold)
            {
                //Set the animator IsWalking bool to false.
                anim.SetBool("IsWalking", false);
            }
            //Else the current magnitude is greater than moveThreshold.
            else
            {
                //Set the animator IsWalking bool to true.
                anim.SetBool("IsWalking", true);
            }
        }
        //Else the maze is finished.
        else
        {
            //Set the animator IsFinisheed bool to true.
            anim.SetBool("IsFinished", true);
        }
    }
    #endregion

    #region States Enum
    //The states for each AI.
    public enum States
    {
        Collecting,
        KeySearch,
        NavigatingToEnd,
    }

    //A reference to the States enum.
    public States agentStates;
    #endregion

    #region Change Area Speed method
    //Change Area Speed
    // - Checks what type of NavMesh the agent is on and applies a certain speed to the agent.
    public void ChangeAreaSpeed()
    {
        //Checks whether a NavMesh was hit.
        _navAgent.SamplePathPosition(-1, 0.0f, out NavMeshHit hitNav);

        //Create an int to indicate if it was grass.
        int grass = 1 << NavMesh.GetAreaFromName("High Cost Grass");
        //Create an int to indicate if it was mud.s
        int mud = 1 << NavMesh.GetAreaFromName("Medium Cost Mud");

        //If the hit NavMesh was grass.
        if (hitNav.mask == grass)
        {
            //Change the agent speed to 1.5f.
            _navAgent.speed = 1.5f;
        }
        //If the hit NavMesh was mud.
        else if (hitNav.mask == mud)
        {
            //Change the agent speed to 2f.
            _navAgent.speed = 2f;
        }
        //Else the hit was a normal ground surface.
        else
        {
            //Change the agent speed to 3.5f.
            _navAgent.speed = 3.5f;
        }
    }
    #endregion
}
