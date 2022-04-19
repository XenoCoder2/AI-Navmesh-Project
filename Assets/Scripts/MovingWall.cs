using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Transform[] wallPoints = new Transform[2];
    Vector2 currentTransform;
    private float _timerCount;
    private float _closeTime;
    public float speed;
    [SerializeField] private float timer;
    public float timeUntilClose;
   

    private void Start()
    {
        _timerCount = timer;
        _closeTime = timeUntilClose;
    }

    // Update is called once per frame
    void Update()
    {
        currentTransform = transform.position;

        timer -= Time.deltaTime;
       
        if (timer <= 0)
        {
            timeUntilClose -= Time.deltaTime;

            if (timeUntilClose <= 0)
            {
                MoveUp(wallPoints[1], wallPoints[0]);
                
            }
            else
            {
                MoveDown(wallPoints[1], wallPoints[0]);
                
            }
        }
    }

    void MoveDown(Transform ground, Transform startPosition)
    {
        if (Vector2.Distance(currentTransform, ground.position) > 0.01f)
        {
            Vector2 directionToGoal = ground.position - transform.position;
            directionToGoal.Normalize();
            transform.position += speed * Time.deltaTime * (Vector3)directionToGoal;
        }
   
    }

    void MoveUp(Transform ground, Transform startPosition)
    {
        if (Vector2.Distance(currentTransform, startPosition.position) > 0.01f)
        {
            Vector2 directionToGoal = startPosition.position - transform.position;
            directionToGoal.Normalize();
            transform.position += speed * Time.deltaTime * (Vector3)directionToGoal;

        }
        else
        {
            timer = _timerCount;
            timeUntilClose = _closeTime;
        }


    }
}
