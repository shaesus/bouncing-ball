using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent OnGameStart = new UnityEvent();

    public static void SendOnGameStart()
    {
        OnGameStart.Invoke();
    }
}
