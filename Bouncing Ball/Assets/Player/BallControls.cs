using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BallControls : MonoBehaviour
{
    public static BallControls Instance { get; private set; }
    
    [HideInInspector] public UnityEvent onBounceCountChange = new UnityEvent();

    public LayerMask groundLayer;
    
    public float BoostDistance => boostDistance;

    [SerializeField] private float boostDistance = 2f;

    [SerializeField] private float defaultBoostForce = 2f;
    
    private Rigidbody _rigidbody;

    private bool _isOnGround = false;
    private bool _canBoost = false;
    
    private float _boostForce;
    private float _lastYVelocityValue = 0;
    
    private int _bounceCount = 0;
    private int _clickCount = 0;

    private float BounceMultiplier => GameManager.Instance.BounceMultiplier;

    private Camera MainCam => Camera.main;
    
    private void Awake()
    {
        Initialize();
        
        GlobalEventManager.OnGameEnd.AddListener(() =>
        {
            _rigidbody.velocity = Vector3.zero;
        });
    }

    private void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _rigidbody = GetComponent<Rigidbody>();

        _boostForce = defaultBoostForce;
    }
    
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        _clickCount++;
                
        _rigidbody.AddForce(Vector3.up * _boostForce * 2, ForceMode.Impulse);
    }

    private void UpdateBouncesCount()
    {
        GameManager.Instance.BouncesLeft--;
        onBounceCountChange.Invoke();
    }
    
    private void Update()
    {
        if (GameManager.Instance.IsGameStarted == false) return;
        
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

        if (Input.GetMouseButton(1))
        {
            var x = MainCam.ScreenToWorldPoint(Input.mousePosition).x;
            x = Mathf.Clamp(x, -2.15f, 2.15f);
            
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        
        CameraFollow.Instance.SetNeedToFollow(transform.position.y >= CameraFollow.Instance.FollowHeight);

        CheckScore();
        CheckBounce();
        KeepInFrame();
    }

    private void KeepInFrame()
    {
        if (transform.position.x > 2.15f)
            transform.position = new Vector3(2.15f, transform.position.y, transform.position.z);
        else if (transform.position.x < -2.15f)
            transform.position = new Vector3(-2.15f, transform.position.y, transform.position.z);
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
                UpdateBouncesCount();
            }
        }

        _lastYVelocityValue = _rigidbody.velocity.y;
    }
    
    private float GetBoostMultiplier(Vector3 position)
    {
        var deltaY = boostDistance - position.y;

        return deltaY * 0.5f * BounceMultiplier;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.IsGameStarted) return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        
            if (GameManager.Instance.BouncesLeft == 0 && GameManager.Instance.IsGameStarted)
            {
                GameManager.Instance.EndGame();
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
                
                _rigidbody.AddForce(Vector3.up * _boostForce, ForceMode.Impulse);
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.EndGame();
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
