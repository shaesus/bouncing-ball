using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public void LoadGameScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
