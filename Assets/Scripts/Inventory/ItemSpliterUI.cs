using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ItemSpliterUI : MonoBehaviour, IScrollHandler
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

    public Action<uint, uint> onOKClick;

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
        ok.onClick.AddListener(Test);
        //ok.onClick.AddListener(() =>
        //{
        //    Debug.Log("OK Button");
        //    onOKClick?.Invoke(targetSlot.Index, ItemSplitCount);    // 어떤 슬롯에서 몇개의 아이템을 옮길지 알려주기
        //    Close();                                                // 아이템 분리창 닫기
        //});

        Button cancel = transform.GetChild(5).GetComponent<Button>();
        cancel.onClick.AddListener(() => Close());

        itemImage = transform.GetChild(6).GetComponent<Image>();
    }
    void Test()
    {
        Debug.Log("OK Button");
        onOKClick?.Invoke(targetSlot.Index, ItemSplitCount);    // 어떤 슬롯에서 몇개의 아이템을 옮길지 알려주기
        Close();
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

    bool IsAreaInside(Vector2 screenPos)
    {
        RectTransform rect = (RectTransform)transform;
        float harfWidth = rect.rect.width * 0.5f;
        Vector2 min = new Vector2(rect.position.x - harfWidth, rect.position.y);
        Vector2 max = new Vector2(rect.position.x + harfWidth, rect.position.y + rect.rect.height*0.5f);
            
        return min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y;
    }
    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if(gameObject.activeSelf)
        {
        Vector2 screenPos = Mouse.current.position.ReadValue();
            if(!IsAreaInside(screenPos))
            {
                Close();
            }
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        if(eventData.scrollDelta.y > 0)
        {
            ItemSplitCount++;
        }
        else
        {
            ItemSplitCount--;
        }
    }
}
