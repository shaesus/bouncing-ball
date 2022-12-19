using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    public TextMeshProUGUI BouncesLeftTMPro;
    public TextMeshProUGUI ScoreTextTMPro;
    
    public GameObject PauseMenu;
    public GameObject PauseButtonGO;

    public Button MainMenuButton;
    
    private GameManager GameManager => GameManager.Instance;
    
    private void Start()
    {
        BouncesLeftTMPro.text = "Bounces Left: " + GameManager.BouncesLeft;
        GlobalEventManager.OnGameStart.AddListener(()=>PauseButtonGO.SetActive(true));
        GlobalEventManager.OnPauseToggle.AddListener(()=>PauseMenu.SetActive(GameManager.PauseManager.IsPaused));
        GlobalEventManager.OnScoreChanged.AddListener(UpdateScoreText);
        GameManager.OnBounceCountChange.AddListener(UpdateBoucesCountText);
        
        MainMenuButton.onClick.AddListener(GlobalEventManager.SendOnGameEnd);
        
        PauseButtonGO.GetComponent<Button>().onClick.AddListener(GameManager.TogglePauseGame);
    }

    private void UpdateBoucesCountText()
    {
        BouncesLeftTMPro.text = "Bounces Left: " + GameManager.BouncesLeft;
    }

    private void UpdateScoreText()
    {
        ScoreTextTMPro.text = GameManager.Score.ToString();
    }
}
