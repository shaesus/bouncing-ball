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
}
