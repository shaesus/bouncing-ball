using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacleSets;

    public GameObject ball;
    
    private float _spawnHeight;

    private float FollowHeight => CameraFollow.Instance.FollowHeight;
    
    private void Start()
    {
        _spawnHeight = FollowHeight;
    }

    private void Update()
    {
        if (ball.transform.position.y >= _spawnHeight - FollowHeight / 2) SpawnObstacleSet();
    }

    public void SpawnObstacleSet()
    {
        _spawnHeight += 16;

        var rndIndex = Random.Range(0, obstacleSets.Length);
        Instantiate(obstacleSets[rndIndex], new Vector3(0, _spawnHeight, 0), Quaternion.identity);
    }
}
