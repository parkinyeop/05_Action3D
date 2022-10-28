using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player => player; // player 읽기전용 프로퍼티

    ItemDataManager itemData;
    public ItemDataManager ItemData => itemData;

    protected override void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();

        player = FindObjectOfType<Player>();
    }
}
