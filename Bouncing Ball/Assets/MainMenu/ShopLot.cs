using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopLot : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI currentValueTMPro;
    public TextMeshProUGUI priceTMPro;

    public Button purchaseButton;

    public Image buttonImage;
    public Image coinImage;
    
    [SerializeField] protected int price;
    
    public void OnBuy()
    {
        slider.value++;
        if (slider.value == slider.maxValue)
        {
            DisableButton();
            return;
        }
        
        price = (int)(price * 1.5f);
        priceTMPro.text = price.ToString();
    }

    protected void DisableButton()
    {
        Utils.ChangeImageAlpha(buttonImage, 0.5f);
        Utils.ChangeImageAlpha(coinImage, 0.5f);
        
        priceTMPro.text = "--";
        
        purchaseButton.enabled = false;
    }
}
