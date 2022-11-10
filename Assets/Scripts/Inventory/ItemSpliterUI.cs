using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemSpliterUI : MonoBehaviour
{

    const int itemCountMin = 1;
    uint itemCountMax = 1;
    uint itemSplitCount = itemCountMin;

    Image itemImage;
    ItemSlot targetSlot;

    TMP_InputField inpuField;
    Slider slider;

    uint ItemSplitCount
    {
        get => itemSplitCount;
        set
        {
            if (itemSplitCount != value)
            {
                itemSplitCount = value;
                itemSplitCount = (uint)Mathf.Max(1, itemSplitCount);

                if (targetSlot != null)
                {
                    itemSplitCount = (uint)Mathf.Min(itemSplitCount, targetSlot.ItemCount - 1);
                }
                inpuField.text = ItemSplitCount.ToString();
                slider.value = itemSplitCount;
            }
        }
    }
    private void Awake()
    {
        inpuField = GetComponentInChildren<TMP_InputField>();
        inpuField.onValueChanged.AddListener((text) => ItemSplitCount = uint.Parse(text));

        slider = GetComponentInChildren<Slider>();
        //slider.onValueChanged.AddListener(ChangeSliderValue);
        slider.onValueChanged.AddListener((value) => ItemSplitCount = (uint)Mathf.RoundToInt(value));

        Button increase = transform.GetChild(1).GetComponent<Button>();
        increase.onClick.AddListener(() => ItemSplitCount++);
        Button decrease = transform.GetChild(2).GetComponent<Button>();
        decrease.onClick.AddListener(() => ItemSplitCount--);
        Button ok = transform.GetChild(4).GetComponent<Button>();
        Button cancel = transform.GetChild(5).GetComponent<Button>();

        itemImage = transform.GetChild(6).GetComponent<Image>();
    }

    //private void ChangeSliderValue(float value)
    //{
    //    ItemSplitCount = (uint)Mathf.RoundToInt(value);
    //}

    private void Start()
    {
        Close();
    }

    public void Open(ItemSlotUI target)
    {
        targetSlot = target.ItemSlot;

        ItemSplitCount = 1;

        itemImage.sprite = targetSlot.ItemData.itemIcon;

        slider.minValue = itemCountMin;
        slider.maxValue = targetSlot.ItemCount - 1;
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
