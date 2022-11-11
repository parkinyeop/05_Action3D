using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;

    Inventory inven;

    ItemSlotUI[] slotUIs;
    TempItemSlotUI tempSlotUI;

    DetailInfoUI detail;

    ItemSpliterUI spliter;

    PlayerInputActions inputActions;

    private void Awake()
    {
        Transform slotParent = transform.GetChild(0);
        slotUIs = new ItemSlotUI[slotParent.childCount];
        //slotUIs = new ItemSlotUI[slotParent.childCount];
        for (int i = 0; i < slotParent.childCount; i++)
        {
            Transform child = slotParent.GetChild(i);
            slotUIs[i] = child.GetComponent<ItemSlotUI>();
        }
        
        tempSlotUI = GetComponentInChildren<TempItemSlotUI>();
        detail = GetComponentInChildren<DetailInfoUI>();
        spliter = GetComponentInChildren<ItemSpliterUI>();
        spliter.onOKClick += OnSplitOK;

        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += spliter.OnMouseClick;
        inputActions.UI.Click.canceled += tempSlotUI.OnDrop;
    }
    private void OnDisable()
    {
        inputActions.UI.Click.canceled -= tempSlotUI.OnDrop;
        inputActions.UI.Click.performed -= spliter.OnMouseClick;
        inputActions.UI.Disable();
    }

    /// <summary>
    /// 입력 받은 인벤토리에 맞게 각종 초기화
    /// </summary>
    /// <param name="plaeyrInven">UI에 표기할 플레이어 인벤토리</param>
    public void InitailizeInventory(Inventory plaeyrInven)
    {
        inven = plaeyrInven;
        Transform slotParent = transform.GetChild(0);
        GridLayoutGroup grid = slotParent.GetComponent<GridLayoutGroup>();
        //기본 사이즈와 다르면 기존 슬롯을 전부 삭제하고 새로 만들기
        if (Inventory.Default_Invetory_Size != inven.SlotCount)
        {
            foreach (var slot in slotUIs)
            {
                Destroy(slot.gameObject);   //기존 슬놋 삭제
            }

            RectTransform rectParent = (RectTransform)slotParent;
            float totalArea = rectParent.rect.width * rectParent.rect.height;
            float slotArea = totalArea / inven.SlotCount;

            float slotSideLength = Mathf.Floor((Mathf.Sqrt(slotArea)) - grid.spacing.x);
            grid.cellSize = new Vector2(slotSideLength, slotSideLength);

            slotUIs = new ItemSlotUI[inven.SlotCount];      //슬롯 배열을 새 크기에 맞게 새로 생성
            for (int i = 0; i < inven.SlotCount; i++)
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
            slotUIs[i].onDragStart += OnItemMoveStart;
            slotUIs[i].onDragEnd += OnItemMoveEnd;
            slotUIs[i].onDragCancel += OnItemMoveCancel;
            slotUIs[i].onClick += OnItemMoveEnd;
            slotUIs[i].onShiftClick += OnItemSplit;
            slotUIs[i].onPointerEnter += OnItemDetailOn;
            slotUIs[i].onPointerExit += OnItemDetailOff;
            slotUIs[i].onPointerMove += OnPointerMove;

        }
        tempSlotUI.InitializeSlot(Inventory.TempSlotIndex, inven.TempSlot);
        tempSlotUI.onTempSlotOpenClose += OnDetailPause;
        tempSlotUI.Close();
    }

  

    private void OnItemMoveStart(uint slotID)
    {
        inven.MoveItem(slotID, Inventory.TempSlotIndex);
        tempSlotUI.Open();
    }
    private void OnItemMoveEnd(uint slotID)
    {
        inven.MoveItem(Inventory.TempSlotIndex, slotID);
        if (tempSlotUI.ItemSlot.IsEmpty)
        {
            tempSlotUI.Close();
        }
        detail.Open(inven[(int)slotID].ItemData);
    }
    private void OnItemSplit(uint slotID)
    {
        spliter.Open(slotUIs[slotID]);
        detail.Close();
        detail.IsPause = true;
    }
    private void OnItemMoveCancel(uint slotID)
    {
        inven.MoveItem(Inventory.TempSlotIndex, slotID);
        if (tempSlotUI.ItemSlot.IsEmpty)
        {
            tempSlotUI.Close();
        }
    }
    private void OnItemDetailOn(uint slotID)
    {
        detail.Open(slotUIs[slotID].ItemSlot.ItemData);
    }
    private void OnItemDetailOff(uint _)
    {
        detail.Close();
    }
    private void OnPointerMove(Vector2 pointerPos)
    {
        if (detail.IsOpen)
        {
            detail.MovePosition(pointerPos);
        }
    }
    private void OnDetailPause(bool isPause)
    {
        detail.IsPause = isPause;
    }
    private void OnSplitOK(uint slotID, uint count)
    {
        inven.MoveItemToTempSlot(slotID, count);
        tempSlotUI.Open();
    }
}
