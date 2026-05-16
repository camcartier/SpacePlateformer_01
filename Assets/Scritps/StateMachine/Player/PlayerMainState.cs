using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainState : PlayerBaseState
{

    Vector2 movement = new Vector2();

    private bool isMoving;

    private bool isJumping;
    //private float maxJumpTime;
    private float jumpTimer;
    public PlayerMainState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.FlyEvent += OnFly;
        stateMachine.InputReader.DashEvent += OnDash;

        stateMachine.previousStateWasJump = false;
        stateMachine.isSliding = false;



        //stateMachine.rb2D.velocity = new Vector2 (0,0);

        //Debug.Log("main");
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.FlyEvent -= OnFly;
        stateMachine.InputReader.DashEvent -= OnDash;
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("main");

        //Debug.Log(stateMachine.ColliderReceiver.isGrounded);

        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0);

        //rotation

        if (movement.x < 0) 
        {stateMachine.Visuals.transform.rotation= new Quaternion(0,0,0,0); }
        if (movement.x > 0)
        {stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0);}


        if(stateMachine.rb2D.velocity.x < 10)
        {
            //le y?

            stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, stateMachine.rb2D.velocity.y);
        }


        //if (stateMachine.InputReader.Jump.ReadValue<float>() > 0)
        //{
        //    stateMachine.wantsToJump = true;
        //}

        //if(stateMachine.wantsToJump && stateMachine.isGrounded)
        //{
        //    stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        //}


        if (stateMachine.rb2D.velocity.y < 0 && !stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }


        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.canFly)
        {
            stateMachine.SwitchState(new PlayerFlyingState(stateMachine));
            return;
            
        }

        if (stateMachine.isBoosted)
        {
            stateMachine.SwitchState(new PlayerAttractedState(stateMachine));
            return;
        }

        if (stateMachine.isSliding)
        {
            stateMachine.SwitchState(new PlayerSlidingState(stateMachine)); return;
        }




    }

    private void OnJump()
    {
        //Debug.Log(stateMachine.ColliderReceiver.isGrounded);
        Debug.Log("jump");

        if(stateMachine.isGrounded == true)
        {
            //Debug.Log("jump grounded");
            stateMachine.isJumping = true;

            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
            
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
