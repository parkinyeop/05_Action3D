using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions inputActions;
    
    float moveSpeed = 1f;
    float moveVertical;
    float moveHorizontal;

    Vector3 input;
    Vector3 lookDirection;
    Vector3 moveDirection;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        moveDirection = new Vector3(-moveHorizontal, 0, -moveVertical);
    }

    private void Update()
    {
        Move();        
    }

    private void Move()
    {
        transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        lookDirection = -moveHorizontal * Vector3.right + -moveVertical * Vector3.forward;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        moveVertical = input.y;
        moveHorizontal = input.x;
    }
}
