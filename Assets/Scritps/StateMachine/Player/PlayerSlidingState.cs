using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlidingState : PlayerBaseState
{
    Vector2 movement = new Vector2();

    public PlayerSlidingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("sliding");
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.FlyEvent += OnFly;
        stateMachine.InputReader.DashEvent += OnDash;

        stateMachine.previousStateWasJump = false;

        stateMachine.PlayerData.currentSlideValue = stateMachine.PlayerData.slideValueStartPoint;
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.FlyEvent -= OnFly;
        stateMachine.InputReader.DashEvent -= OnDash;


    }

    public override void Tick(float deltaTime)
    {


        if (stateMachine.PlayerData.currentSlideValue < stateMachine.PlayerData.slideValueEndPoint)
        {
            stateMachine.PlayerData.currentSlideValue += Time.deltaTime;
        }
        else {
            stateMachine.PlayerData.currentSlideValue = stateMachine.PlayerData.slideValueEndPoint;
        }

        
        //movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0).normalized;
        
        //rotation
        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0);
        //if (movement.x < 0)
        //{ stateMachine.Visuals.transform.rotation = new Quaternion(0, 0, 0, 0); }
        //if (movement.x > 0)
        //{ stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0); }



        stateMachine.rb2D.gravityScale = stateMachine.PlayerData.currentSlideValue;


        if (!stateMachine.ColliderReceiver.isSliding && stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine));
            return;
        }

        if (stateMachine.rb2D.velocity.y <= 0 && !stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }


        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.canFly)
        {
            stateMachine.SwitchState(new PlayerFlyingState(stateMachine));
            return;
            
        }

        
    }

    private void OnJump()
    {
        //Debug.Log(stateMachine.ColliderReceiver.isGrounded);
        //Debug.Log("jump");

        if (stateMachine.isGrounded == true)
        {

            //isJumping = true;

            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
            //stateMachine.rb2D.AddForce(stateMachine.PlayerData.jumpForce, ForceMode2D.Impulse);
        }


    }

    private void OnFly()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerFlyingState(stateMachine));

        }

    }

    private void OnDash()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }

}
