using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private Vector2 movement;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.isGrounded = false;
        stateMachine.isJumping = true;

        stateMachine.rb2D.AddForce(stateMachine.PlayerData.jumpForce, ForceMode2D.Impulse);

        stateMachine.InputReader.DashEvent += OnDash;

        stateMachine.canCoyoteJump = false;
        stateMachine.isSliding = false;

        stateMachine.previousStateWasJump = true;

        
    }

    public override void Exit()
    {
        stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.isJumping = false;
    }

    public override void Tick(float deltaTime)
    {
        //Debug.Log(stateMachine.isGrounded);

        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0).normalized;

        if (stateMachine.isBoosted)
        {
            stateMachine.SwitchState(new PlayerAttractedState(stateMachine));
            return;
        }


        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.canFly)
        {
            stateMachine.SwitchState(new PlayerFlyingState(stateMachine));
            return;
        }


        if (stateMachine.rb2D.velocity.y < 0)
        {
            //stateMachine.rb2D.gravityScale = stateMachine.PlayerData.gravityDownStrength;
            //stateMachine.rb2D.drag = stateMachine.PlayerData.gravityDownDrag;
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }


        stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, stateMachine.rb2D.velocity.y);


    }

    private void OnDash()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }
}
