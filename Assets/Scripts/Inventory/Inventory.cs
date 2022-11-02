using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//인벤토리의 정보만 가지는 클래스
public class Inventory
{
    public const int Default_Invetory_Size = 6;
    ItemSlot[] slots = null;

    ItemDataManager dataManager;

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

        ItemSlot emptySlot = FindEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.AssignSlotItem(data);
            result = true;
            Debug.Log($"인벤토리 {emptySlot.Index}번 슬롯에 {data.itemName} 아이템 추가");
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다");
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

    public void PrintInventory()
    {
        string[] printInventory = new string[slots.Length];
        int[] printCount = new int[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemData != null)
            {
                printInventory[i] = slots[i].ItemData.itemName;
                printCount[i] = (int)slots[i].ItemCount;
            }
            else
            {
                printInventory[i] = "(빈칸)";
            }
        }

        for (int i = 0; i < printInventory.Length; i++)
        {
            Debug.Log($"{printInventory[i]} : {printCount[i]}");
        }

    }
}
