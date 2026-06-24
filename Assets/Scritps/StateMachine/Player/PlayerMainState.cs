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

    private const float CrossFadeDuration = 0.1f;
    private readonly int IdleHash = Animator.StringToHash("Idle3");
    private readonly int WalkHash = Animator.StringToHash("Walk7");

    public PlayerMainState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.FlyEvent += OnFly;
        stateMachine.InputReader.DashEvent += OnDash;
        stateMachine.InputReader.ShootEvent += OnShoot;

        stateMachine.previousStateWasJump = false;
        stateMachine.isSliding = false;

        stateMachine.Animator.Play(IdleHash);

        stateMachine.rb2D.gravityScale = 1f;

        //stateMachine.rb2D.velocity = new Vector2 (0,0);

        //Debug.Log("main");
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.FlyEvent -= OnFly;
        stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.InputReader.ShootEvent -= OnShoot;
        
        
        stateMachine.Animator.Play(IdleHash);

    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.isDead) { stateMachine.SwitchState(new PlayerDeathState(stateMachine)); return; }
        if (stateMachine.isHurt) { stateMachine.SwitchState(new PlayerHurtState(stateMachine)); return; }

        if (stateMachine.isDialog) { stateMachine.SwitchState(new PlayerDialogState(stateMachine)); return; }



        stateMachine.dashDirection2 = new Vector2(stateMachine.InputReader.AerialMovementValue.x , 
                                                  stateMachine.InputReader.AerialMovementValue.y).normalized;


        RaycastHit2D raycasthit = Physics2D.Raycast(stateMachine.transform.position, Vector2.down, 4f);


        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue * stateMachine.SpeedModifier.speedValue, 0);


        

        //rotation

        if (movement.x < 0) 
        {stateMachine.Visuals.transform.rotation= new Quaternion(0,0,0,0); }
        if (movement.x > 0)
        {stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0);}

        //Debug.Log("is on a slope is " +stateMachine.isOnASLope);
        //Debug.Log("sliding is " +stateMachine.isSliding);

        //Debug.Log("slope direction" +stateMachine.slopeDirection);

        if (!stateMachine.isOnASLope)
        {
            
            stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, stateMachine.rb2D.velocity.y);
        }
        else
        {


            if (movement.x == 0)
            {
                stateMachine.rb2D.velocity = Vector2.zero;
            }
            else
            {
                //stateMachine.rb2D.velocity = new Vector2(stateMachine.slopeDirection.x * movement.x * stateMachine.PlayerData.groundedSpeed,
                //                                   stateMachine.slopeDirection.y * stateMachine.rb2D.velocity.y);

                stateMachine.rb2D.velocity = stateMachine.slopeDirection.normalized * movement.x * stateMachine.PlayerData.groundedSpeed;
            }

            if (stateMachine.isGrounded && stateMachine.rb2D.velocity.y <= 0 && stateMachine.isSliding)
            {
                stateMachine.rb2D.AddForce(-raycasthit.normal * stateMachine.PlayerData.slideForce); 
            }

            /*
            if (movement.x < 0)
            {
                stateMachine.rb2D.velocity = new Vector2(stateMachine.slopeDirection.x* movement.x * stateMachine.PlayerData.groundedSpeed - stateMachine.ColliderReceiver.slopeSlowingFactor,
                                                     stateMachine.rb2D.velocity.y);
            }
            
            if (movement.x > 0) 
            {
                stateMachine.rb2D.velocity = new Vector2(stateMachine.slopeDirection.x*movement.x * stateMachine.PlayerData.groundedSpeed + stateMachine.ColliderReceiver.slopeSlowingFactor,
                                                     stateMachine.rb2D.velocity.y);
            }
            */

            

        }

        if (stateMachine.rb2D.velocity.x != 0)
        {
            stateMachine.Animator.Play(WalkHash);
        }
        else
        {
            stateMachine.Animator.Play(IdleHash);
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


        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.canFly && !stateMachine.isInHub)
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
        ///Debug.Log("jump");

        if(stateMachine.isGrounded == true && !stateMachine.isInHub)
        {
            //Debug.Log("jump grounded");
            stateMachine.isJumping = true;

            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
            
        }
       

    }

    private void OnFly()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0 && !stateMachine.isInHub)
        {
            stateMachine.SwitchState(new PlayerFlyingState(stateMachine));
         
        }

    }


    private void OnDash()
    {
        if (stateMachine.dashResetTimer >= stateMachine.PlayerData.dashReset && !stateMachine.isInHub)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }


    private void OnShoot()
    {
        if (!stateMachine.isInHub)
        {
            stateMachine.SwitchState(new PlayerShootingState(stateMachine));
        }
        
    }
}
