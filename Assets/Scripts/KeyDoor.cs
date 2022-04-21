using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Transform[] wallPoints = new Transform[2];
    Vector2 _currentTransform;
    public BoxCollider boxCollide;
    public bool doorActive;
    public int doorValue;
    public string agent;

    void Update()
    {
        _currentTransform = transform.position;

        if (doorActive && Vector2.Distance(_currentTransform, wallPoints[1].position) > 0.01f)
        {
            StartCoroutine(MoveWall(wallPoints[1], wallPoints[0]));
            if (boxCollide.enabled)
            {
                switch (agent)
                {
                    case "Blue":
                        BlueAgent.blueKeyCount -= 1;
                        break;
                    case "Green":
                        GreenAgent.greenKeyCount -= 1;
                        break;
                    case "Yellow":
                        //YellowAgent.yellowKeyCount -= 1;
                        break;
                    default:
                        break;
                }
               
                boxCollide.enabled = false;
            }
        }
    }

    IEnumerator MoveWall(Transform ground, Transform startPosition)
    {
        if (Vector2.Distance(_currentTransform, ground.position) > 0.01f)
        {
            Vector2 directionToGoal = ground.position - transform.position;
            directionToGoal.Normalize();
            transform.position += 3 * Time.deltaTime * (Vector3)directionToGoal;
        }

        yield return new WaitForSeconds(10f);

        Destroy(gameObject);



    }


}
