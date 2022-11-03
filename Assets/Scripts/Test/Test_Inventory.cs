using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Inventory : Test_Base
{
    Inventory inven;
    // Start is called before the first frame update
    void Start()
    {
        inven = new Inventory(10);
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        inven.AddItem(ItemIdCode.Ruby);
        inven.AddItem(ItemIdCode.Emerald);
        inven.AddItem(ItemIdCode.Sapphire);
        inven.AddItem(ItemIdCode.Emerald);
        inven.AddItem(ItemIdCode.Ruby);

    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        inven.PrintInventory();
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        inven.AddItem(ItemIdCode.Ruby);
        inven.AddItem(ItemIdCode.Ruby);
        inven.AddItem(ItemIdCode.Ruby);
        inven.AddItem(ItemIdCode.Ruby);
        inven.AddItem(ItemIdCode.Emerald);
        inven.AddItem(ItemIdCode.Emerald);
        inven.AddItem(ItemIdCode.Emerald);
        inven.AddItem(ItemIdCode.Sapphire);
        inven.PrintInventory(); // 루비4, 에메3, 사파1

        inven.AddItem(ItemIdCode.Ruby, 3);
        inven.AddItem(ItemIdCode.Ruby, 3);

        inven.AddItem(ItemIdCode.Emerald, 4);
        inven.PrintInventory(); // 루비4, 에메3, 사파1 , 루비2, 에메1
        inven.MoveItem(0, 3); 
        inven.PrintInventory();// 루비 1,에메3, 사파1, 루비5, 에메1
        inven.MoveItem(1, 4);
        inven.PrintInventory();//루비1, 에메1, 사파1, 루비5, 에메3
                               //루비3, 에메 2, 사바1, 루비5, 에메3

    }
}
