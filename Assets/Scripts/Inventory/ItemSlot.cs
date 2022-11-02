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
    }
}
