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
    MoneyPanelUI moneyPanel;
    CanvasGroup canvasGroup;
    Button closeButton;
    public Player Owner => inven.Owner;

    public Action onInventoryOpen;
    public Action onInventoryClose;

    private void Awake()
    {
        //itemSlot들을 생성하고 붙일 상위 오브젝트(GridObjectGroup)을 가지고 있음
        Transform slotParent = transform.GetChild(0);
        slotUIs = new ItemSlotUI[slotParent.childCount];    //slotParent의 자식 갯수 만큼 ItemSlotUI배열을 생성
        for (int i = 0; i < slotParent.childCount; i++)
        {
            Transform child = slotParent.GetChild(i);
            slotUIs[i] = child.GetComponent<ItemSlotUI>();  //각 Child 에 ItemSlotUI 콤포넌트를 할당
        }

        tempSlotUI = GetComponentInChildren<TempItemSlotUI>();
        detail = GetComponentInChildren<DetailInfoUI>();
        spliter = GetComponentInChildren<ItemSpliterUI>();
        moneyPanel = GetComponentInChildren<MoneyPanelUI>();
        spliter.onOKClick += OnSplitOK;

        closeButton = transform.GetChild(5).GetComponent<Button>();
        closeButton.onClick.AddListener(Close);
        canvasGroup= GetComponentInChildren<CanvasGroup>();
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += spliter.OnMouseClick;
        inputActions.UI.Click.canceled += tempSlotUI.OnDrop;
        inputActions.UI.InventoryOnOff.performed += inventoryShortCut;
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
    /// <param name="playerInven">UI에 표기할 플레이어 인벤토리</param>
    public void InitailizeInventory(Inventory playerInven)
    {
        inven = playerInven;
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
            slotUIs[i].onClick += OnClick;
            slotUIs[i].onShiftClick += OnItemSplit;
            slotUIs[i].onPointerEnter += OnItemDetailOn;
            slotUIs[i].onPointerExit += OnItemDetailOff;
            slotUIs[i].onPointerMove += OnPointerMove;

        }
        tempSlotUI.InitializeSlot(Inventory.TempSlotIndex, inven.TempSlot);
        tempSlotUI.onTempSlotOpenClose += OnDetailPause;
        tempSlotUI.Close();

        Owner.onEquipItemClear += OnEquipClear;

        Owner.onMoneyChange += moneyPanel.Refresh;
        moneyPanel.Refresh(Owner.Money);

        Close();
    }

    public void Open()
    {
        canvasGroup.alpha= 1.0f;
        canvasGroup.interactable= true;
        canvasGroup.blocksRaycasts= true;
        onInventoryOpen?.Invoke();
    }
    public void Close()
    {
        canvasGroup.alpha= 0.0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        onInventoryClose?.Invoke();
    }
    private void inventoryShortCut(InputAction.CallbackContext _)
    {
        if (canvasGroup.interactable)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    private void OnEquipClear(EquipPartType part)
    {

        foreach (var slotUI in slotUIs)
        {
            ItemData_EquipItem equipItem = slotUI.ItemSlot.ItemData as ItemData_EquipItem;
            if (equipItem != null && equipItem.EquipPart == part)
            {
                slotUI.ClearEquiptMark();
            }
        }
    }

    public bool IsInInventoryArea(Vector2 screenPos)
    {
        bool result = false;

        RectTransform rect = (RectTransform)transform;

        Vector2 min = new(rect.position.x - rect.sizeDelta.x, rect.position.y);
        Vector2 max = new(rect.position.x, rect.sizeDelta.y + rect.position.y);
        return result = (min.x < screenPos.x && screenPos.x < max.x && min.y < screenPos.y && screenPos.y < max.y);
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

    private void OnClick(uint slotID)
    {
        if (tempSlotUI.ItemSlot.IsEmpty)
        {
            ItemSlot useItemSlot = inven[(int)slotID];
            useItemSlot.UseSlotItem(Owner.gameObject);
        }
        else
        {
            OnItemMoveStart(slotID);
        }
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
