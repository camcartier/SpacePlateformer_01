using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyingState : PlayerBaseState
{
    Vector2 movement = new Vector2();

    public PlayerFlyingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("fly");
        stateMachine.flyingParticles.Play();

        stateMachine.rb2D.drag = 10;

        stateMachine.previousStateWasJump = true;
    }

    public override void Exit()
    {
        stateMachine.flyingParticles.Stop();

        stateMachine.rb2D.drag = 1;
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.PlayerRessources.fuelCurrentAmount -= Time.deltaTime;

        if (!stateMachine.canFly && !stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }


        //lacher le bouton
        if (stateMachine.InputReader.Fly.ReadValue<float>() <= 0)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine));
        }

        //quantite de fuel
        if(stateMachine.PlayerRessources.fuelCurrentAmount <= 0)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine));
        }


        movement = stateMachine.InputReader.AerialMovementValue;
        if (movement.x < 0)
        {
            stateMachine.Visuals.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (movement.x > 0)
        {
            stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0);
        }


        stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.aerialSpeed, movement.y * stateMachine.PlayerData.aerialSpeed);
    
    
    }




}
