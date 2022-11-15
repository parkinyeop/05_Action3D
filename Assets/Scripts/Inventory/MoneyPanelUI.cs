using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoneyPanelUI : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    private void Awake()
    {
        moneyText= GetComponentInChildren<TextMeshProUGUI>();
    }
    //private void Start()
    //{
    //    Player player = GameManager.Inst.Player;
    //    player.onMoneyChange += Refresh;
    //}

    public void Refresh(int money)
    {
        moneyText.text = $"{money:N0}";
    }
}
