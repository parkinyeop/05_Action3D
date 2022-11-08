using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempItemSlotUI : ItemSlotUI
{
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
