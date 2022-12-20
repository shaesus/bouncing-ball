using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBounceCountShopLot : ShopLot
{
    private void Start()
    {
        Slider.value = PlayerPrefs.GetFloat("BouncesSliderValue", 0);
        if (!Convert.ToBoolean(PlayerPrefs.GetInt("BouncesButtonEnabled", 1)))
        {
            DisableButton();
        }
        
        UpdateMaxBouncesLot();
    }

    public void UpdateMaxBouncesLot()
    {
        CurrentValueTMPro.text = GameManager.Instance.MaxBounces.ToString() + "x";
        PlayerPrefs.SetFloat("BouncesSliderValue", Slider.value);
        PlayerPrefs.SetInt("BouncesButtonEnabled", PurchaseButton.enabled ? 1 : 0);
    }
}
