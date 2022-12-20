using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuUI : MonoBehaviour
{
    public TextMeshProUGUI moneyTMPro;

    public GameObject mainMenu;
    public GameObject upgradesMenu;

    private List<GameObject> _submenus = new List<GameObject>();
    
    private int Money => GameManager.Instance.Money;
    
    private void Start()
    {
        moneyTMPro.text = Money.ToString();
        
        _submenus.Add(upgradesMenu);
    }

    public void OpenUpgradesMenu()
    {
        mainMenu.SetActive(false);
        upgradesMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        foreach (var menu in _submenus)
        {
            menu.SetActive(false);
        }
        
        mainMenu.SetActive(true);
    }
}

