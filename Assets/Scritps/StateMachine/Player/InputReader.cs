using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPCActions
{
    private Controls controls;

    public event Action JumpEvent, FlyEvent, DashEvent;
    
    public bool jumpIsOver;
    public bool flyIsOver;

    public InputAction Fly, Jump;

    public float GroundedMovementValue { get; private set; }
    public Vector2 AerialMovementValue { get; private set; }

    void Start()
    {
        controls = new Controls(); controls.PC.SetCallbacks(this);

        controls.PC.Enable();

        Fly = controls.PC.Fly;
        Jump = controls.PC.Jump;
    }

    private void OnEnable()
    {
        Fly.Enable();
        Jump.Enable();
    }

    private void OnDisable()
    {
        Fly.Disable();
        Jump.Disable();
    }

    void OnDestroy()
    {
        controls.PC.Disable();
    }


    public void OnMoveGrounded(InputAction.CallbackContext context)
    {
        GroundedMovementValue = context.ReadValue<float>();
    }

    public void OnMoveAerial(InputAction.CallbackContext context)
    {
        AerialMovementValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();

        if (context.canceled)
        {
           jumpIsOver = true;
        }
    }


    public void OnFly(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        FlyEvent?.Invoke();

    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DashEvent?.Invoke();

    }
}
