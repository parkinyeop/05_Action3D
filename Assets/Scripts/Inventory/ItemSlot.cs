using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인벤토리 한칸의 정보를 나타내는 클래스
public class ItemSlot
{
    uint slotIndex;
    ItemData slotsItemData = null;
    uint itemCount = 0;

    public bool IsEmpty => slotsItemData == null;
    public uint Index => slotIndex;

    public ItemData ItemData => slotsItemData;

    public uint ItemCount => itemCount;

    public ItemSlot(uint index)
    {
        slotIndex = index;
    }

    public void AssignSlotItem(ItemData data, uint count = 1)
    {
        itemCount = count;
        slotsItemData = data;
        Debug.Log($"인벤토리 {Index}번 슬롯에 {data.itemName} 아이템 추가");
    }

    public void ClearSlotItem()
    {
        slotsItemData = null;
        itemCount = 0;
        Debug.Log($"인벤토리 {Index}번 슬롯 아이템 삭제");
    }

    public void IncreaseSlotItem(uint count = 1)
    {
        itemCount += count;
        Debug.Log($"인벤토리 {Index}번 슬롯에 {slotsItemData.itemName} 아이템 {count}개 증가");
    }

    public void DecreaseSlotItem(uint count = 1)
    {
        int newCount = (int)ItemCount - (int)count;

        if(newCount < 0)
        {
            ClearSlotItem();
        }
        else
        {
            itemCount -= (uint)newCount;
            Debug.Log($"인벤토리 {Index}번 슬롯에 {slotsItemData.itemName} 아이템 {count}개 감소");
        }
    }
}
