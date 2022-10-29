using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Test_Battle : Test_Base
{
    Player player;
    public TextMeshProUGUI testText;
    int count = 0;

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

    protected override void Test3(InputAction.CallbackContext _)
    {
        testText.text = "basic";
        GameObject obj = ItemFactory.MakeItem(1);
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
    }
    protected override void Test4(InputAction.CallbackContext _)
    {

        testText.text = "Position";
        Vector3 testPos = new Vector3(1, 0, 0);
        ItemFactory.MakeItem((ItemIdCode)0, testPos);
    }

    protected override void Test5(InputAction.CallbackContext _)
    {
        count++;
        testText.text = "Position Noise" + count.ToString();
        Vector3 testPos = new Vector3(1, 0, 0);
        ItemFactory.MakeItem((ItemIdCode)0, testPos, true);
    }
    protected override void Test6(InputAction.CallbackContext _)
    {
        testText.text = "Many";
        Vector3 testPos = new Vector3(1, 0, 1);
        ItemFactory.MakeItems((ItemIdCode)1, 3);
    }

    protected override void Test7(InputAction.CallbackContext _)
    {
        testText.text = "Many & Position";
        Vector3 testPos = new Vector3(1, 0, 1);
        ItemFactory.MakeItems((ItemIdCode)2, testPos, 3);
    }

    protected override void Test8(InputAction.CallbackContext _)
    {
        count++;
        testText.text = "Many & Position Noise" + count.ToString();
        Vector3 testPos = new Vector3(1, 0, 0);
        ItemFactory.MakeItems((ItemIdCode)0, testPos, 3, true);
    }
}
