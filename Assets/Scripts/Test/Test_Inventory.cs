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
        inven.AddItem(ItemIdCode.Sapphire, 2);
        inven.AddItem(ItemIdCode.Sapphire, 4);
    }
}
