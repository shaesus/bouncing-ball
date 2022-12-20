using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BounceIndicator : MonoBehaviour
{
    public BallControls ball;

    public Gradient gradient;

    public Image fill;

    private Rigidbody _ballRb;
    
    private void Awake()
    {
        _ballRb = ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ball.transform.position.y <= ball.BoostDistance && _ballRb.velocity.y < 0)
        {
            fill.fillAmount = (ball.BoostDistance - ball.transform.position.y + 0.5f) / ball.BoostDistance;
            fill.color = gradient.Evaluate(fill.fillAmount);
        }
        else
        {
            fill.fillAmount = 0;
        }
    }
}
