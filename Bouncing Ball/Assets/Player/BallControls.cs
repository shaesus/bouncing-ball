using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class BallControls : MonoBehaviour
{
    public LayerMask groundLayer;

    [SerializeField] private float boostDistance = 2f;

    public float BoostDistance => boostDistance;

    [SerializeField] private float defaultBoostForce = 2f;
    
    private Rigidbody _rigidbody;

    private bool _isOnGround = false;
    private bool _canBoost = false;
    
    private float _boostForce;
    
    private int _clickCount = 0;
    private int _bounceCount = 0;

    private float _lastYVelocityValue = 0;

    private float BounceMultiplier => GameManager.Instance.BounceMultiplier;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _boostForce = defaultBoostForce;
        
        StartCoroutine(WaitForStart());
        
        GlobalEventManager.OnGameEnd.AddListener(() =>
        {
            _rigidbody.velocity = Vector3.zero;
            StartCoroutine(WaitForStart());
        });
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
            
            _boostForce += defaultBoostForce * GetBoostMultiplier(transform.position) * BounceMultiplier;
            boostDistance += 0.5f;
        }

        if (Input.GetMouseButton(1))
        {
            var x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            x = Mathf.Clamp(x, -2.15f, 2.15f);
            
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        
        CameraFollow.Instance.SetNeedToFollow(transform.position.y >= CameraFollow.Instance.FollowHeight);

        CheckScore();
        CheckBounce();
    }

    private void CheckScore()
    {
        var score = (int)transform.position.y;
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.Score >= score)
        {
            return;
        }
        
        GameManager.Instance.Score = score;
        GlobalEventManager.SendOnScoreChanged();
    }
    
    private void CheckBounce()
    {
        if (_lastYVelocityValue == 0 && _rigidbody.velocity.y > 0)
        {
            _bounceCount++;
            if (_bounceCount > 1)
            {
                GameManager.Instance.UpdateBouncesCount();
            }
        }

        _lastYVelocityValue = _rigidbody.velocity.y;
    }
    
    private float GetBoostMultiplier(Vector3 position)
    {
        var deltaY = boostDistance - position.y;

        return deltaY * 0.5f;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with ground!");
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;

            if (GameManager.Instance.IsGameStarted)
            {
                if (GameManager.Instance.BouncesLeft <= 0)
                {
                    GameManager.Instance.EndGame();
                }

                else
                {
                    _rigidbody.velocity = Vector3.zero;
                
                    _rigidbody.AddForce(Vector3.up * _boostForce, ForceMode.Impulse);
                }
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
