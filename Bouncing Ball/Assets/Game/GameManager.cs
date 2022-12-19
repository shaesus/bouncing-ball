using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IPauseHandler
{
    public static GameManager Instance { get; private set; }

    [HideInInspector] public UnityEvent OnBounceCountChange = new UnityEvent();

    public PauseManager PauseManager { get; private set; }
    
    public int Score { get; set; }
    public int BouncesLeft { get; private set; }

    public float BounceMultiplier { get; private set; }

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

        DontDestroyOnLoad(gameObject);

        IsGameStarted = false;
        
        GlobalEventManager.OnGameStart.AddListener(StartGame);
        GlobalEventManager.OnGameEnd.AddListener(() =>
        {
            BouncesLeft = _maxBounces;
            IsGameStarted = false;
            Score = 0;
        });

        BounceMultiplier = 1.5f;
        BouncesLeft = _maxBounces;
        Score = 0;
        
        Initialize();
        
        PauseManager.Register(this);
    }

    public void LvlUpBounceMultiplier()
    {
        BounceMultiplier += 0.5f;
    }

    public void IncreaseBounceCount()
    {
        _maxBounces++;
    }
    
    private void Initialize()
    {
        PauseManager = new PauseManager();
    }
    
    public void UpdateBouncesCount()
    {
        BouncesLeft--;
        OnBounceCountChange.Invoke();
    }
    
    private void StartGame()
    {
        IsGameStarted = true;
    }

    public void EndGame()
    {
        GlobalEventManager.SendOnGameEnd();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void TogglePauseGame()
    {
        PauseManager.SetPaused(!PauseManager.IsPaused);
        
        GlobalEventManager.SendOnPauseToggle();
    }
    
    public void SetPaused(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
