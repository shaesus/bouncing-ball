using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceIndicator : MonoBehaviour
{
    public BallControls Ball;

    public Gradient Gradient;

    public Image Fill;

    private Rigidbody _ballRb;
    
    private void Awake()
    {
        _ballRb = Ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Ball.transform.position.y <= Ball.BoostDistance && _ballRb.velocity.y < 0)
        {
            Fill.fillAmount = (Ball.BoostDistance - Ball.transform.position.y + 0.5f) / Ball.BoostDistance;
            Fill.color = Gradient.Evaluate(Fill.fillAmount);
        }
        else
        {
            Fill.fillAmount = 0;
        }
    }
}
