using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUtils : MonoBehaviour
{
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
