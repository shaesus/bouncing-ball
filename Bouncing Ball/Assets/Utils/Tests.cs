using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour
{
    public void IncreaseMoney() => GameManager.Instance.IncreaseMoney(1000);
}
