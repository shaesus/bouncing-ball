using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    public float FollowHeight { get; private set; }
    
    public GameObject followObject;
    
    private Vector3 _defaultPosition;
    private Vector3 _cameraOffset;

    private bool _needToFollow;

    private Camera MainCam => Camera.main;
    
    private float _cameraAspect;
    
    private void Awake()
    {
        Initialize();
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
        
        _defaultPosition = transform.position;
        FollowHeight = MainCam.orthographicSize * 1.4f - 7f + MainCam.transform.position.y;
        _cameraOffset = new Vector3(0, FollowHeight, 0) - transform.position;

        _needToFollow = false;
    }
    
    private void Update()
    {
        if (_needToFollow)
        {
            transform.position = new Vector3(0, followObject.transform.position.y - _cameraOffset.y,
                transform.position.z);
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
