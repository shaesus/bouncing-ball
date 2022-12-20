using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsUtils : MonoBehaviour
{
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("MultiplierSliderValue");
        PlayerPrefs.DeleteKey("MultiplierButtonEnabled");
        PlayerPrefs.DeleteKey("BouncesButtonEnabled");
        PlayerPrefs.DeleteKey("BouncesSliderValue");
        PlayerPrefs.DeleteKey("BounceMultiplier");
        PlayerPrefs.DeleteKey("MaxBounces");
    }
}
