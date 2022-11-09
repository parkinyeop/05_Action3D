using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DetailInfoUI : MonoBehaviour
{

    TextMeshProUGUI itemName;
    TextMeshProUGUI itemValue;
    TextMeshProUGUI itemDesc;
    Image itemIcon;
    CanvasGroup canvasGroup;
    RectTransform rectWindow;

    float detailWindowHeight;
    float detailWindowWidth;
    Vector2 offset;
    private void Awake()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemValue = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        itemDesc = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectWindow = GetComponent<RectTransform>();

        detailWindowWidth = rectWindow.rect.width;
        detailWindowHeight = rectWindow.rect.height;
        offset = new Vector2(detailWindowWidth / 2, detailWindowHeight / 2);
    }
    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue() + offset;
        //Debug.Log(Mouse.current.position.ReadValue());
        if (Mouse.current.position.ReadValue().x + detailWindowWidth > Screen.width)
        {
            transform.position = new Vector2(transform.position.x - detailWindowWidth, transform.position.y);
        }
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
