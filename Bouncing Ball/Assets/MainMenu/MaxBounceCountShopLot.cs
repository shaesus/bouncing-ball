using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBounceCountShopLot : ShopLot
{
    private int MaxBounces => GameManager.Instance.MaxBounces;
    private int MinBounces => GameManager.Instance.MinBouncesCount;
    private int IncreaseValue => GameManager.Instance.BouncesIncreaseValue;

    private void Awake()
    {
        OnBuy.AddListener(GameManager.Instance.IncreaseBounceCount);
        OnBuy.AddListener(UpdateMaxBouncesLot);
    }

    private void Start()
    {
        slider.value = (MaxBounces - 3) / IncreaseValue;
        price = Utils.CalculatePrice(IncreaseValue, price, MinBounces, MaxBounces);
        priceTMPro.text = price.ToString();
        
        if (slider.value == slider.maxValue)
        {
            DisableButton();
        }
        
        UpdateMaxBouncesLot();
    }

    public void UpdateMaxBouncesLot()
    {
        currentValueTMPro.text = GameManager.Instance.MaxBounces.ToString() + "x";
    }
}
