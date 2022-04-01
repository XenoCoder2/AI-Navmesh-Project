using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{

    float _timer = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Quaternion rotation = new Quaternion(0, -20, 0, 0);
            transform.localRotation = Quaternion.Lerp(transform.rotation, rotation, 3f);

        }
    }

    IEnumerator NavWall()
    {
        Quaternion rotation = new Quaternion(0, -20, 0, 0);
        transform.localRotation = Quaternion.Lerp(transform.rotation, rotation, 3f);
        yield return new WaitForSeconds(10f);
        rotation = new Quaternion(0, 69.508f, 0, 0);
        transform.localRotation = Quaternion.Lerp(transform.rotation, rotation, 3f);
        _timer = 10f;
    }
}
