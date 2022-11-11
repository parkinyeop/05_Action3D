using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;

//인벤토리의 정보만 가지는 클래스
public class Inventory
{
    public const int Default_Invetory_Size = 6;
    public const uint TempSlotIndex = 123123; //slots 의 배열에 들어갈 값이 아니면 되고 어떤 숫자를 써도 된다.

    ItemSlot[] slots = null;
    ItemSlot tempSlot = null;
    ItemDataManager dataManager;


    public int SlotCount => slots.Length;
    public ItemSlot TempSlot => tempSlot;

    Player owner;
    public Player Owner => owner;

    /// <summary>
    /// 특정 번쨰의 ItemSlot을 돌려주는 인덱서
    /// </summary>
    /// <param name="index">돌려줄 슬롯의 위치</param>
    /// <returns>index 번째에 있는 ItemSlot</returns>
    public ItemSlot this[int index] => slots[index];
      
    public Inventory(Player owner, int size = Default_Invetory_Size)
    {
        Debug.Log($"{size} 칸 짜리 인벤토리 생성");
        slots = new ItemSlot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new ItemSlot((uint)i);
        }
        tempSlot = new ItemSlot(TempSlotIndex);
        dataManager = GameManager.Inst.ItemData;

        this.owner= owner;
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
            result = targetSlot.IncreaseSlotItem(out uint _);
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

        if (IsValidSlotIndex(index))
        {
            ItemSlot targetSlot = slots[index];

            if (targetSlot.IsEmpty)             //해당 슬롯에 아이템이 있는가?
            {
                targetSlot.AssignSlotItem(data);
                result = true;
            }
            else
            {
                if (targetSlot.ItemData == data)   //슬롯에 아이템이 있다면 같은 종류인가
                {
                    result = targetSlot.IncreaseSlotItem(out uint _);
                }
                else
                {
                    Debug.Log($"실패: {targetSlot}에 다른 아이템이 있습니다");
                }
            }
        }
        else
        {
            Debug.Log($"{index}잘못된 인덱스 입니다");
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
            Debug.Log($"{slotIndex}잘못된 인덱스 입니다");
        }

        return result;
    }
    /// <summary>
    /// 인벤토리 모든 아이템 비우는 함수
    /// </summary>
    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlotItem();
        }
    }

    public void MoveItem(uint from, uint to)
    {
        if (IsValidAndNotEmptySlotIndex(from) && IsValidSlotIndex(to))
        {
            ItemSlot fromSlot = (from == Inventory.TempSlotIndex) ? TempSlot : slots[from];
            ItemSlot toSlot = (to == Inventory.TempSlotIndex) ? TempSlot : slots[to];

            if (fromSlot.ItemData == toSlot.ItemData)
            {
                //증가를 시도한 갯수
                toSlot.IncreaseSlotItem(out uint overCount, fromSlot.ItemCount);
                // from 에서 증가시도한 갯수만 감소
                fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);
            }
            else
            {
                ItemData tempData = fromSlot.ItemData;
                uint tempCount = fromSlot.ItemCount;
                fromSlot.AssignSlotItem(toSlot.ItemData, toSlot.ItemCount);
                toSlot.AssignSlotItem(tempData, tempCount);
            }
        }
    }

    public void MoveItemToTempSlot(uint slotID, uint count)
    {
        if(IsValidAndNotEmptySlotIndex(slotID))
        {
            ItemSlot fromSlot = slots[slotID];
            fromSlot.DecreaseSlotItem(count);
            tempSlot.AssignSlotItem(fromSlot.ItemData, count);
        }
    }

    ItemSlot FindEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                return slot;
                //break;
            }
        }
        return null;
    }

    public ItemSlot FindSameItem(ItemData itemData)
    {
        ItemSlot findSlot = null;

        foreach (var slot in slots)
        {
            if (slot.ItemData == itemData && slot.ItemCount < slot.ItemData.maxStackCount)
            {
                findSlot = slot;
                break;
            }
        }

        return findSlot;
    }

    private bool IsValidSlotIndex(uint index) => (index < SlotCount) || (index == Inventory.TempSlotIndex);
    bool IsValidAndNotEmptySlotIndex(uint index)
    {
        if (IsValidSlotIndex(index))
        {
            ItemSlot testSlot = (index == TempSlotIndex) ? TempSlot : slots[index];
            return !testSlot.IsEmpty;
        }
        return false;
    }
   
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
