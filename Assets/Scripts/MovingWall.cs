using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    #region Variables
    //The wallPoints variable will store the start and end position of the wall.
    public Transform[] wallPoints = new Transform[2];
    //The current transform of the gameObject.
    Vector2 currentTransform;
    //The timer input saved into _timerCount so it can be remembered.
    private float _timerCount;
    //The close time saved into _closeTime so it can be remembered.
    private float _closeTime;
    [Header("Speed of Wall Movements")]
    //The speed that the wall will open/close.
    public float speed;
    [Header("Timers")]
    //The timer for how long until the wall opens.
    [SerializeField] private float timer;
    //The timer for how long until the wall closes.
    public float timeUntilClose;
    #endregion

    #region Start Method
    //The Start method will save the timer values so they can be used again.
    private void Start()
    {
        //Set _timerCount to the timer's value.
        _timerCount = timer;
        //Set _closeTime to the timeUntilClose's value.
        _closeTime = timeUntilClose;
    }
    #endregion

    #region Update Method
    // Update will run the timer methods and switch between whether the wall should open or close.
    void Update()
    {
        //Set the currentTransform to transform.position.
        currentTransform = transform.position;

        //Count down from timer.
        timer -= Time.deltaTime;
       
        //if the timer is less than or equal to 0.
        if (timer <= 0)
        {
            //Count down from timeUntilClose.
            timeUntilClose -= Time.deltaTime;

            //If timeUntilClose is less than or equal to 0.
            if (timeUntilClose <= 0)
            {
                //Run the MoveUp coroutine and take in the first and second wallPoints transforms.
                MoveUp(wallPoints[1], wallPoints[0]);
                
            }
            //Else
            else
            {
                //Run the MoveDown coroutine and take in the first and second wallPoints transforms.
                MoveDown(wallPoints[1], wallPoints[0]);
                
            }
        }
    }
    #endregion

    #region MoveDown and MoveUp Methods
    //MoveDown 
    // - Takes in two transforms, the ground position and start position.
    // - Moves the wall down to the ground position, opening up a path. 
    void MoveDown(Transform ground, Transform startPosition)
    {
        //If the distance between the currentTransform and the ground position is greater than 0.01f.
        if (Vector2.Distance(currentTransform, ground.position) > 0.01f)
        {
            //Create a Vector2 directionToGoal and set it to ground.position minus the current transform.position of the wall.
            Vector2 directionToGoal = ground.position - transform.position;
            //Normalise the directionToGoal value.
            directionToGoal.Normalize();
            //Apply the values to move the wall down to the ground transform.
            transform.position += speed * Time.deltaTime * (Vector3)directionToGoal;
        }

    }

    //MoveUp
    // - Takes in two transforms, the ground position and start position.
    // - Moves the wall up and back to the startPosition, closing a path.
    void MoveUp(Transform ground, Transform startPosition)
    {
        //If the distance between the currentTransform and the start position is greater than 0.01f.
        if (Vector2.Distance(currentTransform, startPosition.position) > 0.01f)
        {
            //Create a Vector2 directionToGoal and set it to startPosition.position minus the current transform.position of the wall.
            Vector2 directionToGoal = startPosition.position - transform.position;
            //Normalise the directionToGoal value.
            directionToGoal.Normalize();
            //Apply the values to move the wall up to the startPosition transform.
            transform.position += speed * Time.deltaTime * (Vector3)directionToGoal;

        }
        //Else
        else
        {
            //Set the timer back to its original value.
            timer = _timerCount;
            //Set timeUntilClose back to its original value.
            timeUntilClose = _closeTime;
        }


    }
    #endregion
}
