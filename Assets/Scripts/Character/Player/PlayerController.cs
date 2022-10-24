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
    Player player;

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
        player = GetComponent<Player>();
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.MoveModeChange.performed += MoveModeChange;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
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
        if (player.IsAlive)
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

        if (!context.canceled && player.IsAlive)
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

            inputDir.y = -2f;
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
            if (inputDir != Vector3.zero)
            {
                animator.SetFloat("Speed", 1f);
            }
        }
        else
        {
            moveMode = MoveMode.Walk;
            currentSpeed = walkSpeed;
            if (inputDir != Vector3.zero)
            {
                animator.SetFloat("Speed", 0.3f);
            }
        }
    }
    private void OnAttack(InputAction.CallbackContext _)
    {
        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);//현재 재생되고 있는 애니메이션의 진행상태를 얻어옴(0~1)
        if (player.IsAlive)
        {
            int comboState = animator.GetInteger("ComboState");
            comboState++;
            animator.SetInteger("ComboState", comboState);
            animator.SetTrigger("Attack");
        }
    }
}
