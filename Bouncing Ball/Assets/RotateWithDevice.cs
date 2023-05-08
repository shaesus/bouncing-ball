using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithDevice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, Input.gyro.attitude.z, transform.rotation.w);
    }
}
