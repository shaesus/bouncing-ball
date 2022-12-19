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

    [SerializeField] private float _minScale = 0.75f;
    [SerializeField] private float _maxScale = 1f;
    
    private void Awake()
    {
        GlobalEventManager.OnGameStart.AddListener(()=>Destroy(gameObject));
    }

    private void Update()
    {
        var sin = Mathf.Sin(Time.time * _moveSpeedMultiplier);
        
        transform.position = new Vector3(transform.position.x, sin
            * _amplitude + _zeroPointY + 950, transform.position.z);

        var coeff = Mathf.Abs(_maxScale - _minScale) / 2;
        var scaleMultiplier = sin * coeff + (_minScale + coeff);
        
        transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
    }
}
