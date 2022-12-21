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
using Scene = UnityEditor.SearchService.Scene;

public class GameManager : MonoBehaviour, IPauseHandler
{
    public static GameManager Instance { get; private set; }

    public static UnityEvent OnHighScoreChange = new UnityEvent();
    
    public PauseManager PauseManager { get; private set; }
    
    public int Score { get; set; }
    public int HighScore { get; private set; }
    
    public int BouncesLeft { get; set; }
    public int Money { get; private set; }
    
    public float BounceMultiplier { get; private set; }
    public float BounceMinMultiplier { get; private set; } = 1f;
    public float MultiplierIncreaseValue { get; private set; } = 0.5f;
    
    public int MaxBounces { get; private set; }
    public int MinBouncesCount { get; private set; } = 3;
    public int BouncesIncreaseValue { get; private set; } = 1;
    
    public bool IsGameStarted { get; private set; }
    
    private void Awake()
    {
        Initialize();
        
        DontDestroyOnLoad(gameObject);
        GlobalEventManager.OnGameRestart.AddListener(ResetValues);
        GlobalEventManager.OnGameRestart.AddListener(()=>IsGameStarted = false);
        GlobalEventManager.OnGameEnd.AddListener(() =>
        {
            Money += Score;
            PlayerPrefs.SetInt("Money", Money);
        });
        
        PauseManager.Register(this);
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
        
        PauseManager = new PauseManager();
        
        IsGameStarted = false;
        
        BounceMultiplier = PlayerPrefs.GetFloat("BounceMultiplier", BounceMinMultiplier);
        MaxBounces = PlayerPrefs.GetInt("MaxBounces", MinBouncesCount);
        Money = PlayerPrefs.GetInt("Money", 0);
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        
        BouncesLeft = MaxBounces;
        Score = 0;
    }

    public void DecreaseMoney(int value)
    {
        Money -= value;
        PlayerPrefs.SetInt("Money", Money);
    }
    
    public void ResetValues()
    {
        BouncesLeft = MaxBounces;
        IsGameStarted = false;
        Score = 0;
    }
    
    public void IncreaseMoney(int amount)
    {
        Money += amount;
    }
    
    public void LvlUpBounceMultiplier()
    {
        BounceMultiplier += MultiplierIncreaseValue;
        PlayerPrefs.SetFloat("BounceMultiplier", BounceMultiplier);
    }

    public void IncreaseBounceCount()
    {
        MaxBounces += BouncesIncreaseValue;
        BouncesLeft = MaxBounces;
        PlayerPrefs.SetInt("MaxBounces", MaxBounces);
    }
    
    public void StartGame()
    {
        IsGameStarted = true;
        
        GlobalEventManager.SendOnGameStart();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        GlobalEventManager.SendOnGameRestart();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void EndGame()
    {
        if (Score > HighScore)
        {
            OnHighScoreChange.Invoke();
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }

        IsGameStarted = false;
        Debug.Log("Ended game!");
        Time.timeScale = 0f;
        
        GlobalEventManager.SendOnGameEnd();
        //TODO: Game End Menu
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
