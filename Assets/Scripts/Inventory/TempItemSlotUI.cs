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
        if(!ItemSlot.IsEmpty)
        {
            transform.position = Mouse.current.position.ReadValue();
            gameObject.SetActive(true);
            onTempSlotOpenClose?.Invoke(true);
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
        onTempSlotOpenClose?.Invoke(false);
    }
}
