using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions inputActions;
    Vector3 inputDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    public float moveSpeed = 3f;

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
        transform.rotation = targetRotation;
    }


    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir.x = input.x;
        inputDir.z = input.y;
        inputDir.y = 0f;

        Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        inputDir = cameraYRotation * inputDir;

        targetRotation = Quaternion.LookRotation(inputDir);
    }
}
