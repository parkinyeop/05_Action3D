using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI hpText;
    float maxHP;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        Player player = GameManager.Inst.Player;
        maxHP = player.MaxHP;
        player.onHealthChange += OnHealthChange;
    }

    private void OnHealthChange(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);
        slider.value = ratio;

        float hp = maxHP * ratio;
        hpText.text = $"{hp:f0} / {maxHP:f0}";
    }
}
