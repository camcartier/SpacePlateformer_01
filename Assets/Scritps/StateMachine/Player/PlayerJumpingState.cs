using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private float hasLeftGroundTimer = 0.05f;
    private float hasLeftGroundCounter;

    private Vector2 movement;

    private const float CrossFadeDuration = 0.1f;
    private readonly int JumpingHash = Animator.StringToHash("Jump2");

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.DashEvent += OnDash;


        stateMachine.isGrounded = false;
        stateMachine.isJumping = true;
        stateMachine.canCoyoteJump = false;
        stateMachine.isSliding = false;
        stateMachine.previousStateWasJump = true;


        stateMachine.rb2D.AddForce(stateMachine.PlayerData.jumpForce, ForceMode2D.Impulse);

        stateMachine.Animator.Play(JumpingHash);
    }

    public override void Exit()
    {
        stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.isJumping = false;

        hasLeftGroundCounter = 0f;

        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.isDead) { stateMachine.SwitchState(new PlayerDeathState(stateMachine)); }


        if (stateMachine.InputReader.jumpIsOver && stateMachine.rb2D.velocity.y > 0) {

            stateMachine.rb2D.velocity = new Vector2(stateMachine.rb2D.velocity.x, stateMachine.rb2D.velocity.y/1.05f);
        }


        stateMachine.dashDirection2 = new Vector2(stateMachine.InputReader.AerialMovementValue.x,
                                                  stateMachine.InputReader.AerialMovementValue.y).normalized;


        //Debug.Log(stateMachine.isGrounded);
        hasLeftGroundCounter += Time.deltaTime;


        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0).normalized;
        if (movement.x < 0)
        { stateMachine.Visuals.transform.rotation = new Quaternion(0, 0, 0, 0); }
        if (movement.x > 0)
        { stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0); }


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
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        
        if (stateMachine.isGrounded && hasLeftGroundCounter > hasLeftGroundTimer)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine)); return;
        }
        
        stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, stateMachine.rb2D.velocity.y);


    }

    private void OnDash()
    {
        if (stateMachine.dashResetTimer >= stateMachine.PlayerData.dashReset)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }
}
