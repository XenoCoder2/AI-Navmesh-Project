using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Vector3 rotationPerSecond;
    public string agentTag;
    int value = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationPerSecond * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(agentTag))
        {
            value++;
            Destroy(gameObject);
        }
    }

}
