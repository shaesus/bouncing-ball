using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public void LoadGameScene()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetValues();
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetValues();
        SceneManager.LoadScene(0);
    }
}
