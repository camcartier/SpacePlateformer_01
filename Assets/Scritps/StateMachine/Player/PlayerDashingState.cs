using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    Vector2 movement = new Vector2();

    Vector2 dashDir;

    private float timerCounter;

    public PlayerDashingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("is dashing");
        //stateMachine.InputReader.DashEvent += OnDash;

        timerCounter = 0;

        stateMachine.rb2D.velocity = Vector2.zero;

        dashDir = stateMachine.dashDirection.transform.position;

        stateMachine.PlayerRessources.fuelCurrentAmount -= 2f;

        stateMachine.isSliding = false;
    }

    public override void Exit()
    {
        //stateMachine.InputReader.DashEvent -= OnDash;
    }

    public override void Tick(float deltaTime)
    {
        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0);
        //if (movement.x < 0)
        //{ stateMachine.Visuals.transform.rotation = new Quaternion(0, 0, 0, 0); }
        //if (movement.x > 0)
        //{ stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0); }


        timerCounter += Time.deltaTime;

        if (timerCounter > stateMachine.PlayerData.dashDuration )
        {
            if (stateMachine.isGrounded && stateMachine.InputReader.Jump.ReadValue<float>() > 0)
            {
                stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerMainState(stateMachine));
            }

               
        }


        stateMachine.rb2D.transform.position = Vector2.MoveTowards(stateMachine.rb2D.transform.position,
                                                                   dashDir,
                                                                   stateMachine.PlayerData.dashForce * Time.deltaTime);


        //stateMachine.rb2D.transform.position = Vector2.Lerp(stateMachine.rb2D.transform.position,
        //                                            stateMachine.dashDirection.transform.position,
        //                                           .1f * Time.deltaTime);
    }

    private void OnDash()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }

}
