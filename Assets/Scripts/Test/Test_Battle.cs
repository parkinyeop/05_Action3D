using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Test_Battle :  Test_Base
{
    Player player;
    private void Start()
    {
        player = GameManager.Inst.Player;
        player.MP = 20;
        player.HP = 50;
    }
    protected override void Test1(InputAction.CallbackContext _)
    {
        player.ManaRegenerate(50, 3);
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        player.MP += 10;
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        player.MP -= 20;
    }
    protected override void Test4(InputAction.CallbackContext _)
    {
        GameObject[] objs = ItemFactory.MakeItem(ItemIdCode.Sapphire, 2);
        GameObject[] objs2 = ItemFactory.MakeItem(ItemIdCode.Emerald, 3, new Vector3(0, 0, 2f));
        GameObject[] objs3 = ItemFactory.MakeItem(ItemIdCode.Ruby,5, new Vector3(1, 0, 2f), true);
        GameObject[] objs4 = ItemFactory.MakeItem(ItemIdCode.HealingPotion,5, new Vector3(1, 0, 2f), true);
        GameObject[] objs5 = ItemFactory.MakeItem(ItemIdCode.ManaPotion,5, new Vector3(1, 0, 2f), true);

    }

}
