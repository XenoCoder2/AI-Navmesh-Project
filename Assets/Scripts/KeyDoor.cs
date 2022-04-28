using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    #region Variables
    [Tooltip("Element 0 should be the initial position and so Element 1 is the downwards position.")]
    //The points where the wall will move.
    public Transform[] wallPoints = new Transform[2];
    //The current transform of the gameObject.
    Vector2 _currentTransform;
    [Header("Door Collider")]
    //The collider for the door.
    public BoxCollider boxCollide;
    [Header("Door Active")]
    //A check for whether the door is active or not.
    public bool doorActive;
    [Header("Door Value")]
    //The value of the door, it is associated with a key.
    public int doorValue;
    [Header("Targeted Agent")]
    //The agent that will interact with the door (Blue, Green or Yellow).
    public string agent;
    #endregion

    #region Update Method
    //The Update method will update _currentTransform and check whether the door is activated.
    void Update()
    {
        //Update _currentTransform to the transform.position of the gameObject.
        _currentTransform = transform.position;

        //If the doorActive bool is true and the distance between the _currentTransform and the downwards wallPoint is greater than 0.01f.
        if (doorActive && Vector2.Distance(_currentTransform, wallPoints[1].position) > 0.01f)
        {
            //Run the MoveWall coroutine and take in the second transform of wallPoints.
            StartCoroutine(MoveWall(wallPoints[1]));
            //If the box collider is enabled.
            if (boxCollide.enabled)
            {
                //Switch between the agents.
                switch (agent)
                {
                    //If agent is equal to Blue.
                    case "Blue":
                        //Take away 1 from the blueKeyCount.
                        BlueAgent.blueKeyCount -= 1;
                        break;
                    //If agent is equal to Green.
                    case "Green":
                        //Take away 1 from the greenKeyCount.
                        GreenAgent.greenKeyCount -= 1;
                        break;
                    //If agent is equal to Yellow.
                    case "Yellow":
                        //Take away 1 from yellowKeyCount.
                        YellowAgent.yellowKeyCount -= 1;
                        break;
                }
               
                //Disable the box collider.
                boxCollide.enabled = false;
            }
        }
    }
    #endregion

    #region MoveWall Coroutine
    //MoveWall
    // - Take in one transform to indicate the desired position (ground).
    // - Move the wall to the ground transform.
    // - Wait ten seconds then destroy the gameObject.
    IEnumerator MoveWall(Transform ground)
    {
        //If the distance between _currentTransform and the ground.position 
        if (Vector2.Distance(_currentTransform, ground.position) > 0.01f)
        {
            //Create a Vector2 variable named directionToGoal and set its value to ground.position minus the transform.position of the gameObject.
            Vector2 directionToGoal = ground.position - transform.position;
            //Normalise directionToGoal.
            directionToGoal.Normalize();
            //Apply values to transform.position to move it to the ground transform.
            transform.position += 3 * Time.deltaTime * (Vector3)directionToGoal;
        }

        //Wait for ten seconds.
        yield return new WaitForSeconds(10f);

        //Destroy the gameObject.
        Destroy(gameObject);

    }
    #endregion

}
