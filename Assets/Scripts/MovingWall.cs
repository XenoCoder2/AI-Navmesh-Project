using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public Transform[] wallPoints = new Transform[2];
    Vector2 currentTransform;
    public float timerCount;
    public float speed;
    [SerializeField] private float timer;

    // Update is called once per frame
    void Update()
    {
        currentTransform = transform.position;

        timer -= Time.deltaTime;
       
        if (timer <= 0)
        {
            StartCoroutine(MoveWall(wallPoints[1], wallPoints[0]));

        }
    }


    IEnumerator MoveWall(Transform ground, Transform startPosition)
    {
        
        if (Vector2.Distance(currentTransform, ground.position) > 0.01f)
        {
            Vector2 directionToGoal = ground.position - transform.position;
            directionToGoal.Normalize();
            transform.position += speed * Time.deltaTime * (Vector3)directionToGoal;
            
        }

        yield return new WaitForSecondsRealtime(timerCount);

        if (Vector2.Distance(currentTransform, startPosition.position) > 0.01f)
        {
            Vector2 directionToGoal = startPosition.position - transform.position;
            directionToGoal.Normalize();
            transform.position += speed * Time.deltaTime * (Vector3)directionToGoal;

        }

        timer = timerCount;
    }
}
