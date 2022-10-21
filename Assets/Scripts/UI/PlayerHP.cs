using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    public TMP_Text hpText;
    Slider slider;
    Player player;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Start()
    {
        player = GameManager.Inst.Player;
        player.onHealthChange += OnHealthChange;
        //Debug.Log(player.hp);
    }

    private void OnHealthChange(float ratio)
    {
        hpText.text = $"{player.hp}/{player.maxHp}";
        ratio = Mathf.Clamp(ratio,0,1);
        slider.value = ratio;
    }
}
