using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopLot : MonoBehaviour
{
    public Slider Slider;

    public TextMeshProUGUI CurrentValueTMPro;
    public TextMeshProUGUI PriceTMPro;

    public Button PurchaseButton;

    public Image ButtonImage;
    public Image CoinImage;
    
    [SerializeField] private int _price;

    private void Awake()
    {
        PriceTMPro.text = _price.ToString();
    }

    public void OnBuy()
    {
        Slider.value++;
        if (Slider.value == Slider.maxValue)
        {
            DisableButton();
            return;
        }
        
        _price = (int)(_price * 1.5f);

        PriceTMPro.text = _price.ToString();
    }

    private void DisableButton()
    {
        Utils.ChangeImageAlpha(ButtonImage, 0.5f);
        Utils.ChangeImageAlpha(CoinImage, 0.5f);
        
        PriceTMPro.text = "--";
        
        PurchaseButton.enabled = false;
    }
}
