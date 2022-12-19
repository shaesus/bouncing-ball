using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnPauseToggle = new UnityEvent();
    public static UnityEvent OnGameEnd = new UnityEvent();
    public static UnityEvent OnScoreChanged = new UnityEvent();
    
    public static void SendOnGameStart()
    {
        OnGameStart.Invoke();
    }

    public static void SendOnPauseToggle()
    {
        OnPauseToggle.Invoke();
    }

    public static void SendOnGameEnd()
    {
        OnGameEnd.Invoke();
    }
    
    public static void SendOnScoreChanged()
    {
        OnScoreChanged.Invoke();
    }
}
