using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    float currentSpeed = 3f;
    enum MoveMode
    {
        Walk = 0,
        Run
    }

    MoveMode moveMode = MoveMode.Walk;

    public float rotationSpeed = 10f;

    Vector3 inputDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;

    PlayerInputActions inputActions;
    Animator animator;
    CharacterController characterController;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.MoveModeChange.performed += MoveModeChange;
    }



    private void OnDisable()
    {
        inputActions.Player.MoveModeChange.performed -= MoveModeChange;
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Disable();
    }

    private void Start()
    {

    }

    private void Update()
    {
        characterController.Move(currentSpeed * Time.deltaTime * inputDir);
        //characterController.SimpleMove(currentSpeed * inputDir);

        //transform.Translate(currentSpeed * Time.deltaTime * inputDir, Space.World);

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
            if (moveMode == MoveMode.Walk)
            {
                animator.SetFloat("Speed", 0.3f);
            }
            else if (moveMode == MoveMode.Run)
            {
                animator.SetFloat("Speed", 1f);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }
    private void MoveModeChange(InputAction.CallbackContext _)
    {
        if (moveMode == MoveMode.Walk)
        {
            moveMode = MoveMode.Run;
            currentSpeed = runSpeed;
            if(inputDir != Vector3.zero)
            {
                animator.SetFloat("Speed", 1f);
            }
        }
        else
        {
            moveMode = MoveMode.Walk;
            currentSpeed = walkSpeed;
            if(inputDir!= Vector3.zero)
            {
                animator.SetFloat("Speed", 0.3f);
            }
        }
    }
}
