using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public Transform[] wallPoints = new Transform[2];
    public static int keyCount;
    public string agent;
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(agent))
        {
            if (keyCount > 0)
            {
                StartCoroutine(MoveWall());
            }
        }
    }

    IEnumerator MoveWall()
    {
        Vector3 loweredPos = new Vector3(transform.position.x, -7.5f, transform.position.z);

        if (Vector2.Distance(transform.position, loweredPos) > 0.01f)
        {
            Vector2 directionToGoal = loweredPos - transform.position;
            directionToGoal.Normalize();
            transform.position += 3 * Time.deltaTime * (Vector3)directionToGoal;

        }

        keyCount--;
        yield return null;
    }
}
