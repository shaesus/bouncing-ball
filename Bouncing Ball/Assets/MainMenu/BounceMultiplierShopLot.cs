using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMultiplierShopLot : ShopLot
{
    private float BounceMultiplier => GameManager.Instance.BounceMultiplier;
    private float MinMultiplier => GameManager.Instance.BounceMinMultiplier;
    private float IncreaseValue => GameManager.Instance.MultiplierIncreaseValue;

    private void Awake()
    {
        purchaseButton.onClick.AddListener(GameManager.Instance.LvlUpBounceMultiplier);
        purchaseButton.onClick.AddListener(UpdateMultiplierLot);
    }

    private void Start()
    {
        slider.value = (BounceMultiplier - MinMultiplier) / IncreaseValue;
        price = Utils.CalculatePrice(IncreaseValue, price, MinMultiplier, BounceMultiplier);
        priceTMPro.text = price.ToString();
        
        if (slider.value == slider.maxValue)
        {
            DisableButton();
        }
        
        UpdateMultiplierLot();
    }

    public void UpdateMultiplierLot()
    {
        currentValueTMPro.text = GameManager.Instance.BounceMultiplier.ToString() + "x";
    }
}
