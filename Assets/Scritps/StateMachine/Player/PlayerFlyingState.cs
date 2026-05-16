using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyingState : PlayerBaseState
{
    Vector2 movement = new Vector2();

    private float currentDecceleration;

    public PlayerFlyingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("fly");
        stateMachine.flyingParticles.Play();

        stateMachine.InputReader.DashEvent += OnDash;

        stateMachine.rb2D.drag = 10;

        stateMachine.previousStateWasJump = true;
        
        stateMachine.isFlying = true;

        currentDecceleration = stateMachine.PlayerData.flyMaxAcceleration;
    }

    public override void Exit()
    {
        stateMachine.flyingParticles.Stop();

        stateMachine.InputReader.DashEvent -= OnDash;

        stateMachine.rb2D.drag = 1;

        stateMachine.isSliding = false;
        stateMachine.isFlying = false;

    }

    public override void Tick(float deltaTime)
    {

        stateMachine.PlayerRessources.fuelCurrentAmount -= Time.deltaTime;



        if (!stateMachine.canFly && !stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }


        //lacher le bouton
        if (stateMachine.InputReader.Fly.ReadValue<float>() <= 0 && !stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
        else if (stateMachine.InputReader.Fly.ReadValue<float>() <= 0 && stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine));
            return;
        }

        //quantite de fuel
        if(stateMachine.PlayerRessources.fuelCurrentAmount <= 0 && !stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
        else if (stateMachine.PlayerRessources.fuelCurrentAmount <= 0 && stateMachine.isGrounded)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine));
            return;
        }


        movement = stateMachine.InputReader.AerialMovementValue;

        //rotation
        if (movement.x < 0)
        {
            stateMachine.Visuals.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        if (movement.x > 0)
        {
            stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        if(currentDecceleration > .5f)
        {
            currentDecceleration -= stateMachine.PlayerData.flyDecceleration * Time.deltaTime;
        }


        stateMachine.rb2D.velocity = new Vector2(movement.x * stateMachine.PlayerData.XaerialSpeed, movement.y * stateMachine.PlayerData.YaerialSpeed + currentDecceleration);


        //Vector2 velocity = stateMachine.rb2D.velocity;
        //float decelleration = stateMachine.PlayerData.flyDecceleration * Time.deltaTime;

        //if(velocity.x > 0) 
        //{ velocity.x -= decelleration; 
        //    if (velocity.x < 0) 
        //    { velocity.x = 0f; } 
        //}
        //else if (velocity.x < 0) 
        //{ velocity.x += decelleration; 
        //    if (velocity.x > 0 ) 
        //    { velocity.x = 0f; }
        //}


    }


    private void OnDash()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }

}
