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
    }
    protected override void Test1(InputAction.CallbackContext _)
    {
        player.Defence(50);
    }
    protected override void Test2(InputAction.CallbackContext _)
    {
        player.HP = 100;
    }
}
