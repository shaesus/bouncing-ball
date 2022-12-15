using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class BallControls : MonoBehaviour
{
    public LayerMask groundLayer;

    [SerializeField] private float boostDistance = 2f;
    [SerializeField] private float defaultBoostForce = 2f;
    
    private Rigidbody _rigidbody;

    private bool _isOnGround = false;
    private bool _canBoost = false;
    
    private float _boostForce;
    
    private int _clickCount = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _boostForce = defaultBoostForce;
        
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GlobalEventManager.SendOnGameStart();
                _clickCount++;
                
                _rigidbody.AddForce(Vector3.up * _boostForce * 2, ForceMode.Impulse);
                yield break;
            }

            yield return null;
        }
    }
    
    private void Update()
    {
        _canBoost = Physics.Raycast(transform.position, Vector3.down, boostDistance, groundLayer);

        if (!_canBoost)
        {
            _clickCount = 0;
        }
        
        if ((Input.GetMouseButtonDown(0) || Input.touchCount >= 1)
            && _canBoost && !_isOnGround && _clickCount == 0 && _rigidbody.velocity.y < 0)
        {
            _clickCount++;
            
            _boostForce += defaultBoostForce * GetBoostMultiplier(transform.position);
            boostDistance += 0.5f;
        }

        if (transform.position.y >= CameraFollow.Instance.FollowHeight)
        {
            CameraFollow.Instance.SetNeedToFollow(true);
        }
        else
        {
            CameraFollow.Instance.SetNeedToFollow(false);
        }
    }

    private float GetBoostMultiplier(Vector3 position)
    {
        var deltaY = boostDistance - position.y;

        return deltaY * 0.5f;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
            
            if (GameManager.Instance.IsGameStarted)
            {
                _rigidbody.velocity = Vector3.zero;
                
                _rigidbody.AddForce(Vector3.up * _boostForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * boostDistance);
    }
}
