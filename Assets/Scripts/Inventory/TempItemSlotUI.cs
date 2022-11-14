using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TempItemSlotUI : ItemSlotUI
{
    public Action<bool> onTempSlotOpenClose;

    public override void InitializeSlot(uint id, ItemSlot slot)
    {
        onTempSlotOpenClose = null;

        base.InitializeSlot(id, slot);
    }
    private void Start()
    {
        //Vector2 screen = Mouse.current.position.ReadValue();
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
        if (!itemSlot.IsEmpty)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, LayerMask.GetMask("Ground")))
            {
                Debug.Log(hit.point);

                ItemFactory.MakeItem((int)ItemSlot.ItemData.id,(int)itemSlot.ItemCount, hit.point, true);
                ItemSlot.ClearSlotItem();
                Close();
            }
        }
    }
}
