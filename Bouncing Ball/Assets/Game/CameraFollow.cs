using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    public float FollowHeight { get; private set; }
    
    public GameObject FollowObject;
    
    private Vector3 _defaultPosition;
    private Vector3 _cameraOffset;

    private bool _needToFollow = false;
    
    private float _cameraAspect;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _defaultPosition = transform.position;
        FollowHeight = Camera.main.orthographicSize * 1.4f - 0.5f;
        _cameraOffset = new Vector3(0, FollowHeight, 0) - transform.position;
    }

    private void Update()
    {
        if (_needToFollow)
        {
            transform.position = FollowObject.transform.position - _cameraOffset;
        }
    }

    public void SetNeedToFollow(bool needToFollow)
    {
        _needToFollow = needToFollow;

        if (_needToFollow == false)
        {
            transform.position = _defaultPosition;
        }
    }
}
