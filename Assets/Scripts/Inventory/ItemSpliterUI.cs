using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class ItemSpliterUI : MonoBehaviour
{
    uint itemSplitCount = 1;

    Image itemImage;
    ItemSlot targetSlot;

    TMP_InputField inpuField;
    Slider slider;

    private void Awake()
    {
        inpuField = GetComponentInChildren<TMP_InputField>();
        slider = GetComponentInChildren<Slider>();

        Button increase = transform.GetChild(1).GetComponent<Button>();
        Button decrease = transform.GetChild(2).GetComponent<Button>();
        Button ok = transform.GetChild(4).GetComponent<Button>();
        Button cancel = transform.GetChild(5).GetComponent<Button>();
        itemImage = transform.GetChild(6).GetComponent<Image>();
    }

    private void Start()
    {
        inpuField.text = itemSplitCount.ToString();
        Close();
    }

    public void Open(ItemSlotUI target)
    {
        targetSlot = target.ItemSlot;
        itemImage.sprite = target.ItemSlot.ItemData.itemIcon;
        slider.maxValue = target.ItemSlot.ItemCount-1;
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void IncreseCount()
    {
        itemSplitCount += 1;
        inpuField.text = itemSplitCount.ToString();
        if(itemSplitCount >= slider.maxValue)
        {
            itemSplitCount = (uint)slider.maxValue -1;
        }
    }
     public void DecreaseCount()
    {
        itemSplitCount -= 1;
        inpuField.text = itemSplitCount.ToString();
        if (itemSplitCount < slider.minValue)
        {
            itemSplitCount = (uint)slider.minValue;
        }
    }
    public void SlideValue()
    {
        inpuField.text = ((int)slider.value).ToString();
    }
}
