using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailInfoUI : MonoBehaviour
{

    TextMeshProUGUI itemName;
    TextMeshProUGUI itemValue;
    TextMeshProUGUI itemDesc;
    Image itemIcon;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemValue = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        itemDesc = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open(ItemData itemData)
    {
        if (itemData != null)
        {
            itemIcon.sprite = itemData.itemIcon;
            itemName.text = itemData.itemName;
            itemValue.text = itemData.value.ToString();
            itemDesc.text = itemData.itemDescription;
            canvasGroup.alpha = 1.0f;
        }
    }
    public void Close()
    {
        canvasGroup.alpha = 0.0f;
    }

}
