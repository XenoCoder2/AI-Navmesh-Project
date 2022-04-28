using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    #region Variables
    //The rotation of the object.
    public Vector3 rotationPerSecond;
    //The value of the object.
    public int value = 1;
    #endregion

    #region Update Method
    // Update will rotate the object by rotationPerSecond every frame.
    void Update()
    {
        //Rotate the object by rotationPerSecond multiplied by deltaTime.
        transform.Rotate(rotationPerSecond * Time.deltaTime);
    }
    #endregion
}
