using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    uint id;        //몇번째 슬롯인가
    protected ItemSlot itemSlot;    // UI와 연결된 아이템 슬롯
    Image itemImage;
    public uint ID => id;
    public ItemSlot ItemSlot => itemSlot;
    public TextMeshProUGUI itemCountText;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }
    /// <summary>
    /// 슬롯 초기화 함수
    /// </summary>
    /// <param name="id"></param>
    /// <param name="slot"></param>
    public void InitializeSlot(uint id, ItemSlot slot)
    {
        this.id = id;
        this.itemSlot = slot;
        this.itemSlot.onSlotItemChange = Refresh;

        Refresh();
    }

    void Refresh()
    {
        if (itemSlot.IsEmpty)
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
        }
        else
        {
            itemImage.sprite = ItemSlot.ItemData.itemIcon;
            itemImage.color = Color.white;
        }
    }

    public void Resize(float iconSize)
    {
        RectTransform rectTransform = (RectTransform)itemImage.gameObject.transform;
        rectTransform.sizeDelta = new Vector2(iconSize, iconSize);
    }
}
