using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    ItemDataManager itemData;
    InventoryUI inventoryUI;

    public Player Player => player; // player 읽기전용 프로퍼티
    public ItemDataManager ItemData => itemData;
    public InventoryUI InvenUI => inventoryUI;

    protected override void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();
        player = FindObjectOfType<Player>();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }
}
