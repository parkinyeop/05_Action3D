using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMP : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI mpText;
    float maxMP;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        mpText = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        Player player = GameManager.Inst.Player;
        maxMP = player.MaxMP;
        player.onMPChange += OnMPChange;
    }

    private void OnMPChange(float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);
        slider.value = ratio;

        float mp = maxMP * ratio;
        mpText.text = $"{mp:f0} / {maxMP:f0}";
    }
}
