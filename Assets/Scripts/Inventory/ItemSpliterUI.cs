using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        Close();
    }

    public void Open(ItemSlotUI target)
    {
        targetSlot = target.ItemSlot;
        itemImage.sprite = target.ItemSlot.ItemData.itemIcon;
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
