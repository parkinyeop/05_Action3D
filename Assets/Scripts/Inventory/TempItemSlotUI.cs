using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempItemSlotUI : ItemSlotUI
{
    private void Start()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
    }


    public void Open()
    {
        if(!ItemSlot.IsEmpty)
        {
            gameObject.SetActive(true);
        }
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
