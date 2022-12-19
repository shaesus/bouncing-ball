using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGameText : MonoBehaviour
{
    [SerializeField] private float _moveSpeedMultiplier = 2f;
    [SerializeField] private float _amplitude = 50f;
    [SerializeField] private float _zeroPointY = 200f;

    private void Awake()
    {
        GlobalEventManager.OnGameStart.AddListener(()=>Destroy(gameObject));
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * _moveSpeedMultiplier)
            * _amplitude + _zeroPointY + 950, transform.position.z);
    }
}
