using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMultiplierShopLot : ShopLot
{
    private void Start()
    {
        Slider.value = PlayerPrefs.GetFloat("MultiplierSliderValue", 0);
        if (!Convert.ToBoolean(PlayerPrefs.GetInt("MultiplierButtonEnabled", 1)))
        {
            DisableButton();
        }
        
        UpdateMultiplierLot();
    }

    public void UpdateMultiplierLot()
    {
        CurrentValueTMPro.text = GameManager.Instance.BounceMultiplier.ToString() + "x";
        PlayerPrefs.SetFloat("MultiplierSliderValue", Slider.value);
        PlayerPrefs.SetInt("MultiplierButtonEnabled", PurchaseButton.enabled ? 1 : 0);
    }
}
