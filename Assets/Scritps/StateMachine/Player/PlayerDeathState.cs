using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    private float timerCounter;

    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.rb2D.velocity = Vector2.zero;
        stateMachine.gameManager.playerIsDead = true;
        stateMachine.SwitchState(new PlayerMainState(stateMachine));
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        

        /*
        timerCounter += Time.deltaTime;

        if (timerCounter > stateMachine.PlayerData.timeBeforeRespawn)
        {
            stateMachine.gameManager.playerIsDead = true;
            
        }
        */
    }


}
