using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IMainGamePCActions
{
    private Controls controls;

    public event Action JumpEvent, FlyEvent, DashEvent, ShootEvent, ActionsEvent;
    
    public bool jumpIsOver;
    public bool flyIsOver;

    public InputAction Fly, Jump;

    public float GroundedMovementValue { get; private set; }
    public Vector2 AerialMovementValue { get; private set; }

    void Start()
    {
        controls = new Controls(); controls.MainGamePC.SetCallbacks(this);

        controls.MainGamePC.Enable();

        Fly = controls.MainGamePC.Fly;
        Jump = controls.MainGamePC.Jump;
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
        controls.MainGamePC.Disable();
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
        //Debug.Log("jump input");
        if (!context.performed) { JumpEvent?.Invoke(); jumpIsOver = false; }
        //Debug.Log("jump input after");
        

        if (context.canceled)
        {
           jumpIsOver = true;
        }
        

        /*
        //Debug.Log("jump input");
        if (!context.performed) { return; }
        //Debug.Log("jump input after");
        JumpEvent?.Invoke();

        if (context.canceled)
        {
            jumpIsOver = true;
        }*/
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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        ShootEvent?.Invoke();
    }

    public void OnActions(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        ActionsEvent?.Invoke();
    }
}
