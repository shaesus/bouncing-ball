using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationAngle = 5f;
    
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationAngle);
    }
}
