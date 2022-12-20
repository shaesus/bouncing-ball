using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static void ChangeImageAlpha(Image image, float a)
    {
        var tempColor = image.color;
        tempColor.a = a;
        image.color = tempColor;
    }

    public static int CalculatePrice(float increaseCoeff, int price, float defaultValue, float currentValue)
    {
        var count = (currentValue - defaultValue) / increaseCoeff;
        for (int i = 0; i < count; i++)
        {
            price = (int)(price * 1.5f);
        }

        return price;
    }
}
