using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using Unity.VisualScripting;
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
        for (int i = 0; i < size; i++)
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
        ItemSlot targetSlot = FindSameItem(data);
        if (targetSlot != null)
        {
            targetSlot.IncreaseSlotItem();
            return true;
        }
        else
        {
            ItemSlot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                emptySlot.AssignSlotItem(data);
                result = true;
            }
            else
            {
                Debug.Log("인벤토리가 가득 찼습니다");
            }

        }

        return result;
    }

    public bool AddItem(ItemIdCode code, uint index)
    {
        return AddItem(dataManager[code], index);
    }

    public bool AddItem(ItemData data, uint index)
    {
        bool result = false;
        ItemSlot targetSlot = slots[index];

        if (IsValidSlotIndex(index))
        {
            if (targetSlot.IsEmpty)             //해당 슬롯에 아이템이 있는가?
            {
                targetSlot.AssignSlotItem(data);
                Debug.Log($"{index}에 새로운 아이템{data}를 추가하였습니다");
                result = true;
            }
            else if (targetSlot.ItemData == data)   //슬롯에 아이템이 있다면 같은 종류인가
            {
                targetSlot.IncreaseSlotItem();
                Debug.Log($"{index}에 아이템{data}를 추가하였습니다");
                result = true;
            }
        }
        else
        {
            IsValidSlotIndex(index);
        }

        return result;
    }
    /// <summary>
    /// 인벤토리 아이템 제거 
    /// </summary>
    /// <param name="slotIndex">제거할 슬롯</param>
    /// <param name="decreaseCount"> 제거할 갯수</param>
    /// <returns>성공/실패</returns>
    public bool RemoveItam(uint slotIndex, uint decreaseCount = 1)
    {
        bool result = false;
        if (IsValidSlotIndex(slotIndex)) //적절한 인덱스 확인
        {
            ItemSlot slot = slots[slotIndex];
            slot.DecreaseSlotItem(decreaseCount);
            result = true;
        }
        else
        {
            Debug.LogError($"{slotIndex}잘못된 인덱스 입니다");
        }
        return result;
    }
    public bool ClearItem(uint slotIndex)
    {
        bool result = false;
        if (IsValidSlotIndex(slotIndex))
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
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                return slot;
                break;
            }
        }
        return null;
    }

    public ItemSlot FindSameItem(ItemData itemData)
    {
        ItemSlot findSlot = null;

        for (int i = 0; i < SlotCount; i++)
        {
            if (slots[i].ItemData == itemData)
            {
                findSlot = slots[i];
                break;
            }
        }

        return findSlot;
    }

    private bool IsValidSlotIndex(uint index) => (index < SlotCount);
    public void PrintInventory()
    {
        string printText = "[";

        for (int i = 0; i < SlotCount; i++)
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

        ItemSlot lastSlot = slots[SlotCount - 1];
        if (!lastSlot.IsEmpty)
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
