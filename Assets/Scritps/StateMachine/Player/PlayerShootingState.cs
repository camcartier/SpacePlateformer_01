using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingState : PlayerBaseState
{
    private float timer = 0.25f;
    private float timerCounter;

    public PlayerShootingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.rb2D.velocity = Vector3.zero;

        stateMachine.instantiator.InstantiateBullet(stateMachine.bullet, stateMachine.bulletStartPoint.transform.position, Quaternion.identity);
        Debug.Log("shooting");

        //stateMachine.light2D.enabled = true;
    }

    public override void Exit()
    {
        //stateMachine.light2D.enabled = false;
    }

    public override void Tick(float deltaTime)
    {
        timerCounter += Time.deltaTime;

        if (timerCounter > timer)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine));
        }
    }


}
