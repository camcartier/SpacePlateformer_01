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

    private ParticleSystem instantiatedPS;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.InputReader.DashEvent += OnDash;
        stateMachine.InputReader.ShootEvent += OnShoot;

        stateMachine.isGrounded = false;
        stateMachine.isJumping = true;
        stateMachine.canCoyoteJump = false;
        stateMachine.isSliding = false;
        stateMachine.previousStateWasJump = true;
        stateMachine.canShoot = true;

        stateMachine.rb2D.AddForce(stateMachine.PlayerData.jumpForce, ForceMode2D.Impulse);

        stateMachine.Animator.Play(JumpingHash);
        //mon son est trop moche
        //stateMachine.jumpSound.Play();

        //stateMachine.trailRenderer.emitting = true;
        //deactivated parce que pas assez joli
        /*instantiatedPS =  stateMachine.instantiator.InstantiatePS(stateMachine.jumpParticles, 
            stateMachine.rb2D.transform.position, 
            Quaternion.identity);
        instantiatedPS.Play();*/
    }

    public override void Exit()
    {
        stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.InputReader.ShootEvent -= OnShoot;
        stateMachine.isJumping = false;
        stateMachine.canShoot = true;

        hasLeftGroundCounter = 0f;

        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.isDead) { stateMachine.SwitchState(new PlayerDeathState(stateMachine)); return; }
        if (stateMachine.isHurt) { stateMachine.SwitchState(new PlayerHurtState(stateMachine)); return; }

        if (stateMachine.isDialog) { stateMachine.SwitchState(new PlayerDialogState(stateMachine)); return; }

        if (stateMachine.InputReader.jumpIsOver && stateMachine.rb2D.velocity.y > 0) {

            stateMachine.rb2D.velocity = new Vector2(stateMachine.rb2D.velocity.x * stateMachine.SpeedModifier.speedValue, stateMachine.rb2D.velocity.y/1.05f);
        }


        stateMachine.dashDirection2 = new Vector2(stateMachine.InputReader.AerialMovementValue.x,
                                                  stateMachine.InputReader.AerialMovementValue.y).normalized;


        //Debug.Log(stateMachine.isGrounded);
        hasLeftGroundCounter += Time.deltaTime;


        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue * stateMachine.SpeedModifier.speedValue, 0).normalized;
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
        
        stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, 
                                                 stateMachine.rb2D.velocity.y);


    }

    private void OnDash()
    {
        if (stateMachine.dashResetTimer >= stateMachine.PlayerData.dashReset)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }
    private void OnShoot()
    {
        if (stateMachine.canShoot)
        {
            stateMachine.instantiator.InstantiateBullet(stateMachine.bullet, stateMachine.bulletStartPoint.transform.position, Quaternion.identity);
            stateMachine.canShoot = false;
            //not working bcause velocity is being rewritten each frame
            //stateMachine.rb2D.AddForce(new Vector2(-50, 0), ForceMode2D.Impulse);
        }

    }

}
