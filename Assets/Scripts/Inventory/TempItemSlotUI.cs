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
            Player player = GameManager.Inst.Player;
            Vector3 playerPos = player.transform.position;
            float playerPosX = playerPos.x + player.itemPickupRange;
            float playerPosz = playerPos.z + player.itemPickupRange;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask("Ground")))
            {
                //Debug.Log(hit.point);
                if (hit.point.x > playerPos.x + player.itemPickupRange )
                {
                    ItemFactory.MakeItem((int)ItemSlot.ItemData.id,
                        (int)itemSlot.ItemCount, new Vector3(playerPosX, hit.point.y, hit.point.z), true);
                    Debug.Log($"x가 큰 경우 HIT :{hit.point} , Player : {playerPos}");
                }
                else if (hit.point.z > playerPos.z + player.itemPickupRange)
                {
                    ItemFactory.MakeItem((int)ItemSlot.ItemData.id,
                       (int)itemSlot.ItemCount, new Vector3(hit.point.x, hit.point.y, playerPosz), true);
                    Debug.Log($"z가 큰 경우 HIT :{hit.point} , Player : {playerPos}");
                }
                else
                {
                    ItemFactory.MakeItem((int)ItemSlot.ItemData.id, (int)itemSlot.ItemCount, hit.point, true);
                }
                ItemSlot.ClearSlotItem();
                Close();
            }
        }
    }
}
