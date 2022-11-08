using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;

    Inventory inven;

    ItemSlotUI[] slotUIs;
    TempItemSlotUI tempSlotUI;

    private void Awake()
    {
        slotUIs = GetComponentsInChildren<ItemSlotUI>();
        tempSlotUI = GetComponentInChildren<TempItemSlotUI>();
    }
    /// <summary>
    /// 입력 받은 인벤토리에 맞게 각종 초기화
    /// </summary>
    /// <param name="plaeyrInven">UI에 표기할 플레이어 인벤토리</param>
    public void InitailizeInventory(Inventory plaeyrInven)
    {
        inven = plaeyrInven;
        //Transform slotParent = GameObject.FindObjectOfType<ItemSlotUI>().transform.parent;
        Transform slotParent = transform.GetChild(0);
        GridLayoutGroup grid = slotParent.GetComponent<GridLayoutGroup>();
        //기본 사이즈와 다르면 기존 슬롯을 전부 삭제하고 새로 만들기
        if (Inventory.Default_Invetory_Size != inven.SlotCount)
        {
            foreach(var slot in slotUIs)
            {
                Destroy(slot.gameObject);   //기존 슬놋 삭제
            }
            
            RectTransform rectParent = (RectTransform)slotParent;
            float totalArea = rectParent.rect.width * rectParent.rect.height;
            float slotArea = totalArea / inven.SlotCount;

            float slotSideLength = Mathf.Floor((Mathf.Sqrt(slotArea))-grid.spacing.x);
            grid.cellSize = new Vector2(slotSideLength, slotSideLength);

            slotUIs = new ItemSlotUI[inven.SlotCount];      //슬롯 배열을 새 크기에 맞게 새로 생성
            for(int i = 0; i < inven.SlotCount; i++)
            {
                GameObject obj = Instantiate(slotPrefab, slotParent);   //슬롯을 하나씩 생성
                obj.name = $"{slotPrefab.name}_{i}";
                slotUIs[i] = obj.GetComponent<ItemSlotUI>();
            }
        }
        
        for (int i = 0; i < inven.SlotCount; i++)
        {
            slotUIs[i].InitializeSlot((uint)i, inven[i]);   //각 슬롯 초기화
            slotUIs[i].Resize(grid.cellSize.x * 0.75f);     //슬폿 크기에 맞게 내부 이미지 리사이즈
            slotUIs[i].onDragStart += OnItemDragStart;
            slotUIs[i].onDragEnd += OnItemDragEnd;
        }
        tempSlotUI.InitializeSlot(Inventory.TempSlotIndex, inven.TempSlot);
        tempSlotUI.Close();
    }

    uint temp = 0;
    private void OnItemDragStart(uint slotID)
    {
        inven.MoveItem(slotID,Inventory.TempSlotIndex);
        tempSlotUI.Open();
    }
    private void OnItemDragEnd(uint slotID)
    {
        tempSlotUI.Close();
        inven.MoveItem(Inventory.TempSlotIndex, slotID);
    }
}
