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

        stateMachine.previousStateWasJump = false;
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.FlyEvent -= OnFly;
    }

    public override void Tick(float deltaTime)
    {

        //Debug.Log(stateMachine.ColliderReceiver.isGrounded);

        //rotation
        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0);
        if(movement.x < 0) 
        {stateMachine.Visuals.transform.rotation= new Quaternion(0,0,0,0); }
        if (movement.x > 0)
        {stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0);}


        if(stateMachine.rb2D.velocity.x < 10)
        {
            //le y?

            stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, stateMachine.rb2D.velocity.y);
        }



        /*
        if (isJumping)
        {
            if(jumpTimer < stateMachine.PlayerData.maxJumpTime)
            {
                jumpTimer += Time.deltaTime;
                stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, stateMachine.PlayerData.jumpVelocity);
            }
            else { stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.groundedSpeed, 0);
                jumpTimer = 0f ; isJumping = false;
            }
        }*/



        if (stateMachine.rb2D.velocity.y < 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }


        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerFlyingState(stateMachine));
        }

        if (stateMachine.isBoosted)
        {
            stateMachine.SwitchState(new PlayerAttractedState(stateMachine));
        }


    }

    private void OnJump()
    {
        //Debug.Log(stateMachine.ColliderReceiver.isGrounded);
        //Debug.Log("jump");

        if(stateMachine.ColliderReceiver.isGrounded == true)
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




    //private void FaceMovementDirecton()
    //{
    //    if (movement.x == 0) { return; }

    //    if (movement.x < 0)
    //    {
    //        this.stateMachine.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
    //    }
    //    else
    //    {
    //        this.stateMachine.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    //    }
    //}
}
