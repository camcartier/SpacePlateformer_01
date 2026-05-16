using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    Vector2 movement = new Vector2();
    Vector2 fallForce= new Vector2();

    float fallAcceleration = -10f;

    private float timer = 1.5f;
    private float timerCounter;

    private float coyoteTimerCounter;

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.PlayerData.fallVector = new Vector2(0, -2);

        //canCoyoteJump = true;
        coyoteTimerCounter = stateMachine.PlayerData.coyoteTime;

        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DashEvent += OnDash;
        //if (stateMachine.rb2D.gravityScale > 1f)
        //{

        //}
        //stateMachine.rb2D.gravityScale = 1f;


    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DashEvent -= OnDash;

        stateMachine.rb2D.gravityScale = 1f;

        stateMachine.canCoyoteJump = false;
        
        timerCounter = 0f;

    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log("falling");

        coyoteTimerCounter -= Time.deltaTime;
        if(coyoteTimerCounter <= 0) { stateMachine.canCoyoteJump= false; }



        //Debug.Log(fallForce.y);
        if (stateMachine.PlayerData.fallVector.y > -200)
        {
            stateMachine.PlayerData.fallVector.y += stateMachine.PlayerData.fallForce * Time.deltaTime;
        }
        else { stateMachine.PlayerData.fallVector.y = -200; }
        

        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0).normalized;


        if (movement.x != 0)
        {
            stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.fallingSpeed, stateMachine.PlayerData.fallVector.y);
        }
        else
        {
            stateMachine.rb2D.velocity = new Vector2(stateMachine.rb2D.velocity.x, stateMachine.PlayerData.fallVector.y);
        }

        //rotation
        if (movement.x < 0)
        { stateMachine.Visuals.transform.rotation = new Quaternion(0, 0, 0, 0); }
        if (movement.x > 0)
        { stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0); }

        
        
        if (stateMachine.isBoosted) { stateMachine.SwitchState(new PlayerAttractedState(stateMachine)); return; }


        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.canFly )  
        {stateMachine.SwitchState(new PlayerFlyingState(stateMachine)); return; }




        
        //if (stateMachine.ColliderReceiver.isGrounded == true)
        //{

        //     stateMachine.SwitchState(new PlayerMainState(stateMachine)); return;
        //}


        
        //if (stateMachine.isGrounded == true)
        //{
        //    if (stateMachine.InputReader.Jump.ReadValue<float>() > 0 || stateMachine.isJumping)
        //    {
        //        //stateMachine.isJumping = false;
        //        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));

        //    }
        //    else
        //    { stateMachine.SwitchState(new PlayerMainState(stateMachine)); return; }
        //}

        if (stateMachine.isGrounded == true)
        {

            stateMachine.SwitchState(new PlayerMainState(stateMachine)); return;
        }


        if (stateMachine.canCoyoteJump && !stateMachine.previousStateWasJump == true)
        {
            if (stateMachine.InputReader.Jump.ReadValue<float>() > 0 || stateMachine.isJumping)
            {
                stateMachine.isJumping = false;
                stateMachine.SwitchState(new PlayerJumpingState(stateMachine)); return;
                
            }

        }


        if (stateMachine.rb2D.transform.eulerAngles.z > 0)
            {
                stateMachine.rb2D.transform.rotation = new Quaternion(0, 0, 0, 0);
            }



        //is it useful after the change i made? idk
        if (stateMachine.isJumping)
        {
            timerCounter += Time.deltaTime;
            if(timerCounter > timer)
            {
                stateMachine.isJumping = false;
            }
        }


    }

    private void OnJump()
    {
        stateMachine.isJumping = true;


    }
    private void OnDash()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }

}
