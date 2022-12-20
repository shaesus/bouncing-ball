using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    public TextMeshProUGUI bouncesLeftTMPro;
    public TextMeshProUGUI scoreTextTMPro;

    public TextMeshProUGUI resultScoreTMPro;
    public TextMeshProUGUI highScoreTMPro;

    public GameObject newHighScoreText;
    
    public GameObject endGameMenu;
    public GameObject pauseMenu;
    public GameObject pauseButtonGo;
    public GameObject restartButtonGoHUD;
    public GameObject startGamePanel;
    
    public Button restartButton;
    
    private GameManager GameManager => GameManager.Instance;

    private void Start()
    {
        UpdateBoucesCountText();
        
        GlobalEventManager.OnGameStart.AddListener(()=>
        {
            pauseButtonGo.SetActive(true);
            restartButtonGoHUD.SetActive(true);
            Destroy(startGamePanel);
        });

        GlobalEventManager.OnPauseToggle.AddListener(()=>pauseMenu.SetActive(GameManager.PauseManager.IsPaused));
        GlobalEventManager.OnScoreChanged.AddListener(UpdateScoreText);
        GlobalEventManager.OnGameEnd.AddListener(()=>
        {
            endGameMenu.SetActive(true);
            resultScoreTMPro.text = "Score: " + GameManager.Score;
            highScoreTMPro.text = "High Score: " + GameManager.HighScore;
        });
        
        BallControls.Instance.onBounceCountChange.AddListener(UpdateBoucesCountText);
        
        GameManager.OnHighScoreChange.AddListener(()=>newHighScoreText.SetActive(true));
        
        restartButton.onClick.AddListener(GameManager.RestartGame);
        
        restartButtonGoHUD.GetComponent<Button>().onClick.AddListener(GameManager.RestartGame);
        pauseButtonGo.GetComponent<Button>().onClick.AddListener(GameManager.TogglePauseGame);
    }

    private void UpdateBoucesCountText()
    {
        bouncesLeftTMPro.text = "Bounces Left: " + GameManager.BouncesLeft;
    }

    private void UpdateScoreText()
    {
        scoreTextTMPro.text = GameManager.Score.ToString();
    }
}
