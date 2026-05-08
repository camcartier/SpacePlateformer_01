using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    Vector2 movement = new Vector2();
    Vector2 fallForce= new Vector2();

    float fallAcceleration = -2f;

    private float timer = 1.5f;
    private float timerCounter;

    private float coyoteTimerCounter;

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //canCoyoteJump = true;
        coyoteTimerCounter = stateMachine.PlayerData.coyoteTime;

        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.rb2D.gravityScale = 1f;
        Debug.Log("falling");
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;

        stateMachine.rb2D.gravityScale = 1f;

        stateMachine.canCoyoteJump = false;
    }

    public override void Tick(float deltaTime)
    {
        coyoteTimerCounter -= Time.deltaTime;
        if(coyoteTimerCounter <= 0) { stateMachine.canCoyoteJump= false; }


        //Debug.Log(fallForce.y);
        if (fallForce.y > -50)
        {
            fallForce.y += fallAcceleration * Time.fixedDeltaTime;
        }
        else { fallForce.y = -50; }
        

        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0).normalized;

        stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.fallingSpeed, fallForce.y);


        if (stateMachine.isBoosted) {stateMachine.SwitchState(new PlayerAttractedState(stateMachine));}

        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.canFly )  
        {stateMachine.SwitchState(new PlayerFlyingState(stateMachine));}



        if (stateMachine.ColliderReceiver.isGrounded == true)
            if (stateMachine.InputReader.Jump.ReadValue<float>() > 0 || stateMachine.isJumping)
            {
                stateMachine.isJumping = false;
                stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
            }
            else
            { stateMachine.SwitchState(new PlayerMainState(stateMachine)); }
        else if (stateMachine.canCoyoteJump && !stateMachine.previousStateWasJump == true)
        {
            if (stateMachine.InputReader.Jump.ReadValue<float>() > 0 || stateMachine.isJumping)
            {
                stateMachine.isJumping = false;
                stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
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
}
