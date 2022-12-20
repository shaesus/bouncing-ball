using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class StartGameText : MonoBehaviour
{
    [SerializeField] private float moveSpeedMultiplier = 2f;
    [SerializeField] private float amplitude = 50f;
    [SerializeField] private float zeroPointY = 200f;

    [SerializeField] private float minScale = 0.75f;
    [SerializeField] private float maxScale = 1f;
    
    private void Awake()
    {
        GlobalEventManager.OnGameStart.AddListener(()=>Destroy(gameObject));
    }

    private void Update()
    {
        var sin = Mathf.Sin(Time.time * moveSpeedMultiplier);
        
        transform.position = new Vector3(transform.position.x, sin
            * amplitude + zeroPointY + 950, transform.position.z);

        var coeff = Mathf.Abs(maxScale - minScale) / 2;
        var scaleMultiplier = sin * coeff + (minScale + coeff);
        
        transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
    }
}
