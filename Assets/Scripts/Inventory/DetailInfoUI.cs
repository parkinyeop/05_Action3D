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

    bool isPause = false;
    public float time = 0.1f;
    public bool IsOpen => (canvasGroup.alpha > 0);
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            {
                if (isPause)
                {
                    Close();
                }
            }
        }
    }
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
        if (!isPause && itemData != null)
        {
            itemIcon.sprite = itemData.itemIcon;
            itemName.text = itemData.itemName;
            itemValue.text = itemData.value.ToString()+"골드";
            itemDesc.text = itemData.itemDescription;
            float delta = 0;
            while(Mathf.Sin(delta)< 1)
            {
                delta += Time.deltaTime * time;
                canvasGroup.alpha = Mathf.Sin(delta);
                //    canvasGroup.alpha = delta;
                   Debug.Log(canvasGroup.alpha);
            }
            MovePosition(Mouse.current.position.ReadValue());
        }
    }
    public void Close()
    {
        //float delta = 1;
        //while (delta > 0)
        //{
        //    delta -= Time.deltaTime * time;
        //    canvasGroup.alpha = delta;
        //}
        canvasGroup.alpha = 0;
    }
    public void MovePosition(Vector2 pos)
    {
        RectTransform rect = (RectTransform)transform;
        transform.position = pos;

        if (pos.x + rect.rect.width > Screen.width)
        {
            pos.x -= rect.rect.width;
        }
        transform.position = pos;
    }
}
