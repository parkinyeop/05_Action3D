using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;

//인벤토리 한칸의 정보를 나타내는 클래스
public class ItemSlot
{
    uint slotIndex;
    ItemData slotsItemData = null;
    uint itemCount = 0;

    /// <summary>
    /// 슬롯에 들어있는 아이템 데이터 프로퍼티
    /// </summary>
    public ItemData ItemData
    {
        get => slotsItemData;
        private set
        {
            if (slotsItemData != value) //슬롯 아이템이 변경 있을때
            {
                slotsItemData = value;
                onSlotItemChange?.Invoke(); // 델리게이트에서 연결된 함수들 실행(UI갱신용)
            }
        }
    }

    public uint ItemCount
    {
        get => itemCount;
        private set
        {
            if (itemCount != value)  // 아이템 갯수가 변경 있을때
            {
                itemCount = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    public Action onSlotItemChange;

    public bool IsEmpty => slotsItemData == null;
    public uint Index => slotIndex;
    public ItemSlot(uint index)
    {
        slotIndex = index;
    }

    public void AssignSlotItem(ItemData data, uint count = 1)
    {
        if (data != null)
        {
            ItemCount = count;
            ItemData = data;
            // Debug.Log($"인벤토리 {Index}번 슬롯에 {data.itemName} 아이템 추가");
        }
        else
        {
            ClearSlotItem();
        }
    }

    public void ClearSlotItem()
    {
        ItemData = null;
        ItemCount = 0;
        //Debug.Log($"인벤토리 {Index}번 슬롯 아이템 삭제");
    }
    /// <summary>
    /// 슬롯의 아이템 증가
    /// </summary>
    /// <param name="increaseCount"> 갯수 </param>
    /// <returns>최대값을 넘은 수를 반환</returns>
    public bool IncreaseSlotItem(out uint overCount, uint increaseCount = 1)
    {
        bool result;
        int over = 0;

        over = (int)(ItemCount + increaseCount - ItemData.maxStackCount);
        if (over > 0)
        {
            //아이템의 최대 갯수를 넘김
            ItemCount = ItemData.maxStackCount; //해당 아이템 슬롯에 최대값 적용
            overCount = (uint)over;             //넘친 갯수 저장
            result = false;
            //Debug.Log($"인벤토리 {slotIndex}번 슬롯에 \"{ItemData.itemName}\" 아이템 최대치까지 증가. 현재 {ItemCount}개. {over}개 넘침.");
        }
        else
        {
            ItemCount += increaseCount;
            overCount = 0;      //underFlow 방지
            result = true;
            //Debug.Log($"인벤토리 {Index}번 슬롯에 {ItemData.itemName} 아이템 {increaseCount}개 증가");
        }
        return result;
    }
    /// <summary>
    /// 슬롯의 아이템 감소
    /// </summary>
    /// <param name="count"> 갯수 </param>
    public void DecreaseSlotItem(uint count = 1)
    {

        int newCount = (int)ItemCount - (int)count;

        if (newCount < 1)
        {
            ClearSlotItem();
        }
        else
        {
            ItemCount = (uint)newCount;
            //Debug.Log($"인벤토리 {Index}번 슬롯에 {ItemData.itemName} 아이템 {count}개 감소");
        }
    }

    public void UseSlotItem(GameObject target = null)
    {
        IUsable usable =  ItemData as IUsable;

        if(usable != null)
        {
            if(usable.Use(target))
            {
                DecreaseSlotItem();
            }
        }
    }
}
