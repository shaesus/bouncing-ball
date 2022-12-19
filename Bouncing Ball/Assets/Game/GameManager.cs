using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI BouncesLeftTMPro;
    
    public int Score { get; set; }
    public int BouncesLeft { get; private set; }

    [SerializeField] private int _maxBounces;
    
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

        BouncesLeft = _maxBounces;
        Score = 0;

        BouncesLeftTMPro.text = "Bounces Left: " + BouncesLeft;
    }

    public void UpdateBouncesCount()
    {
        Debug.Log("Updated bounces text!");
        BouncesLeft--;
        BouncesLeftTMPro.text = "Bounces Left: " + BouncesLeft;
    }
    
    private void StartGame()
    {
        IsGameStarted = true;
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
