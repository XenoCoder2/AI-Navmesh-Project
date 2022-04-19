using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Vector3 rotationPerSecond;
    public int value = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationPerSecond * Time.deltaTime);
    }

}
