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
        Test1(_);                   //루비2, 에메2, 사파1
        inven.PrintInventory();
        //inven.AddItem(ItemIdCode.Sapphire, 3);
        //inven.PrintInventory();     //루비2, 에메2, 사파1 , 사파1
        //inven.MoveItem(3, 2);
        //inven.PrintInventory();     // 루비2, 에메2, 사파2
        inven.MoveItem(0, 1);
        inven.PrintInventory();     // 루비2, 사파2, 에메2
    }
}
