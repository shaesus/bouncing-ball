using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public bool IsGameStarted { get; private set; }

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
        
        IsGameStarted = false;
        GlobalEventManager.OnGameStart.AddListener(StartGame);
    }

    private void StartGame()
    {
        IsGameStarted = true;
    }
}
