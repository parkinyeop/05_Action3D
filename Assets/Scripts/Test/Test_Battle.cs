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
        //GameObject[] objs = ItemFactory.MakeItem(ItemIdCode.Coin_Bronze, 2);
        //GameObject[] objs2 = ItemFactory.MakeItem(ItemIdCode.Coin_Silver, 3, new Vector3(0, 0, 2f));
        //GameObject[] objs3 = ItemFactory.MakeItem(ItemIdCode.Coin_Gold, 5, new Vector3(1, 0, 2f), true);
        //GameObject[] objs4 = ItemFactory.MakeItem(ItemIdCode.HealingPotion, 5, new Vector3(1, 0, 2f), true);
        //GameObject[] objs5 = ItemFactory.MakeItem(ItemIdCode.ManaPotion, 5, new Vector3(1, 0, 2f), true);
        GameObject[] objs6 = ItemFactory.MakeItem(ItemIdCode.IronSword, 1, new Vector3(1, 0, 2f), true);
        //GameObject[] objs7 = ItemFactory.MakeItem(ItemIdCode.SilverSword, 1, new Vector3(1, 0, 2f), true);
        //GameObject[] objs8 = ItemFactory.MakeItem(ItemIdCode.RoundSheild, 1, new Vector3(1, 0, 2f), true);
        //GameObject[] objs9 = ItemFactory.MakeItem(ItemIdCode.IronSheild, 1, new Vector3(1, 0, 2f), true);

    }

}
