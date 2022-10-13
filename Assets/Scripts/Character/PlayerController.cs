using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;

    Vector3 inputDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void Start()
    {

    }

    private void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * inputDir, Space.World);

        //transform.rotation = targetRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir.x = input.x;
        inputDir.z = input.y;
        inputDir.y = 0f;

        if (!context.canceled)
        {
            //카메라의 Y축 회전만 뽑음
            Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            inputDir = cameraYRotation * inputDir;  //inputDir과 카메라 방향을 일치 시킨다

            targetRotation = Quaternion.LookRotation(inputDir);
        }
    }
}
