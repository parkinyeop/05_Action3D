using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TempItemSlotUI : ItemSlotUI
{
    Player owner;
    InventoryUI invenUI;
    public Action<bool> onTempSlotOpenClose;

    public override void InitializeSlot(uint id, ItemSlot slot)
    {
        onTempSlotOpenClose = null;
        invenUI = GameManager.Inst.InvenUI;
        owner = invenUI.Owner;

        base.InitializeSlot(id, slot);
    }

    private void Start()
    {
        //Vector2 screen = Mouse.current.position.ReadValue();
        //InventoryUI invenUI = GetComponentInParent<InventoryUI>();
        //owner = invenUI.Owner;
    }

    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }
    public void Open()
    {
        if (!ItemSlot.IsEmpty)
        {
            transform.position = Mouse.current.position.ReadValue();
            onTempSlotOpenClose?.Invoke(true);
            gameObject.SetActive(true);
        }
    }
    public void Close()
    {
        onTempSlotOpenClose?.Invoke(false);
        gameObject.SetActive(false);
    }

    public void OnDrop(InputAction.CallbackContext _)
    {
            Vector2 screenPos = Mouse.current.position.ReadValue();
        if (!invenUI.IsInInventoryArea(screenPos)&&!itemSlot.IsEmpty)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Ground")))
            {
                Vector3 dropDir = hit.point - owner.transform.position;
                Vector3 dropPos = hit.point;

                if (dropDir.sqrMagnitude > owner.itemPickupRange * owner.itemPickupRange)
                {
                    dropPos = dropDir.normalized * owner.itemPickupRange + owner.transform.position;
                }

                ItemFactory.MakeItem((int)ItemSlot.ItemData.id, (int)itemSlot.ItemCount, dropPos, true);
                ItemSlot.ClearSlotItem();
                Close();
            }
        }
    }
}
