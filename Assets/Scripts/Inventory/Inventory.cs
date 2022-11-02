using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인벤토리의 정보만 가지는 클래스
public class Inventory
{
    public const int Default_Invetory_Size = 6;
    ItemSlot[] slots = null;
       
    ItemDataManager dataManager;

    public int SlotCount => slots.Length;

    public Inventory(int size = Default_Invetory_Size)
    {
        Debug.Log($"{size} 칸 짜리 인벤토리 생성");
        slots = new ItemSlot[size];
        for(int i=0; i<size; i++)
        {
            slots[i] = new ItemSlot((uint)i);
        }

        dataManager = GameManager.Inst.ItemData;
    }

    public bool AddItem(ItemIdCode code)
    {
        return AddItem(dataManager[code]);
    }

    public bool AddItem(ItemData data)
    {
        bool result = false;

        ItemSlot emptySlot = FindEmptySlot();
        if(emptySlot != null)
        {
            emptySlot.AssignSlotItem(data);
            result = true;
           
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다");
        }

        return result;
    }

    public bool ClearItem(uint slotIndex)
    {
        bool result = false;
        if(IsValidDlotIndex(slotIndex))
        {
            ItemSlot slot = slots[slotIndex];
            slot.ClearSlotItem();
            return true;
        }
        else
        {
            Debug.LogError($"{slotIndex}잘못된 인덱스 입니다");
        }

        return result;
    }

    ItemSlot FindEmptySlot()
    {
        foreach(var slot in slots)
        {
            if(slot.IsEmpty)
            {
                return slot;
                break;
            }
        }
        return null;
    }

    private bool IsValidDlotIndex(uint index) => (index < SlotCount);
    public void PrintInventory()
    {
        string printText = "[";

        for(int i = 0; i<SlotCount; i++)
        {
            if (!slots[i].IsEmpty)
            {
                printText += $"{slots[i].ItemData.itemName}({slots[i].ItemCount})";
            }
            else
            {
                printText += "(빈칸)";
            }
            printText += ",";
        }

        ItemSlot lastSlot = slots[SlotCount-1];
        if(!lastSlot.IsEmpty)
        {
            printText += $"{lastSlot.ItemData.itemName}({lastSlot.ItemCount})]";
        }
        else
        {
            printText += "(빈칸)]";
        }

        Debug.Log(printText);
    }
}
