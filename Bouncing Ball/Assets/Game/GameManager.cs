using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IPauseHandler
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI BouncesLeftTMPro;

    public GameObject PauseMenu;
    public GameObject PauseButton;
    
    public PauseManager PauseManager { get; private set; }
    
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
        
        Initialize();
        
        PauseManager.Register(this);
    }

    private void Initialize()
    {
        PauseManager = new PauseManager();
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
        
        PauseButton.SetActive(true);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void TogglePauseGame()
    {
        if (PauseManager.IsPaused)
        {
            PauseManager.SetPaused(false);
        }
        else
        {
            PauseManager.SetPaused(true);
        }
    }
    
    public void SetPaused(bool isPaused)
    {
        PauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
