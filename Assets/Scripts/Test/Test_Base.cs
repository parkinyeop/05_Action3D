using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Base : MonoBehaviour
{
    PlayerInputActions inputActions;
    protected virtual void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.Test_1.performed += Test1;
        inputActions.Test.Test_2.performed += Test2;
        inputActions.Test.Test_3.performed += Test3;
        inputActions.Test.Test_4.performed += Test4;
        inputActions.Test.Test_5.performed += Test5;
    }
    private void OnDisable()
    {
        inputActions.Test.Disable();
        inputActions.Test.Test_1.performed -= Test1;
        inputActions.Test.Test_2.performed -= Test2;
        inputActions.Test.Test_3.performed -= Test3;
        inputActions.Test.Test_4.performed -= Test4;
        inputActions.Test.Test_5.performed -= Test5;
    }                                      

    protected virtual void Test1(InputAction.CallbackContext _)
    {                                                        
    }                                                        
    protected virtual void Test2(InputAction.CallbackContext _)
    {                                                        
    }                                                        
    protected virtual void Test3(InputAction.CallbackContext _)
    {                                                        
    }                                                        
    protected virtual void Test4(InputAction.CallbackContext _)
    {                                                        
    }                                                        
    protected virtual void Test5(InputAction.CallbackContext _)
    {
    }
   
}
