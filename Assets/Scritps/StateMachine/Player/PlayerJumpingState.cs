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
        stateMachine.rb2D.AddForce(stateMachine.PlayerData.jumpForce, ForceMode2D.Impulse);

        stateMachine.canCoyoteJump = false;

        stateMachine.previousStateWasJump = true;

        //Debug.Log("jump");
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
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


}
