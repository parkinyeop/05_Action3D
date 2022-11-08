using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;

public class TempItemSlotUI : ItemSlotUI
{
    private void Start()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
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
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
