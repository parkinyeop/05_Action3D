using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Enemy : Test_Base
{
    public Enemy enemy;
    protected override void Test1(InputAction.CallbackContext _)
    {
        enemy.Test();
    }
}
