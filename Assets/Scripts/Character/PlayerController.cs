using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum MoveMode { Walk = 0, Run = 1 }

    public float moveSpeed = 3f;
    public float rotationSpeed = 10f;
    float boostSpeed = 2;
    MoveMode moveMode;

    Animator animator;

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
        inputActions.Player.Run.performed += OnRunInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Run.performed -= OnRunInput;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        moveMode = MoveMode.Walk;
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
        animator.SetFloat("Speed", 0.1f);
        if (!context.canceled)
        {
            //카메라의 Y축 회전만 뽑음
            Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            inputDir = cameraYRotation * inputDir;  //inputDir과 카메라 방향을 일치 시킨다

            targetRotation = Quaternion.LookRotation(inputDir);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
    }
    private void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.performed && moveMode != MoveMode.Run)
        {
            moveSpeed *= boostSpeed;
            animator.SetFloat("Speed", 1.0f);
            moveMode = MoveMode.Run;
           
        }
        else
        {
            moveSpeed /= boostSpeed;
            animator.SetFloat("Speed", 0.1f);
            moveMode = MoveMode.Walk;
        }
    }
}
