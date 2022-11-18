using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    [Range(1f, 10f)]
    public float distance = 10f;
    PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Effect.Enable();
        inputActions.Effect.CursorMove.performed += OnMouseMove;
    }

    private void OnDisable()
    {
        inputActions.Effect.CursorMove.performed -= OnMouseMove;
        inputActions.Effect.Disable();
    }

    void OnMouseMove(InputAction.CallbackContext context)
    {
        Vector3 mousePos = context.ReadValue<Vector2>();
        mousePos.z = distance;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

}
