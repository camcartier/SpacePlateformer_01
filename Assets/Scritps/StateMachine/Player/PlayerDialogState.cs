using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogState : PlayerBaseState
{
    public PlayerDialogState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("dialog");

        stateMachine.rb2D.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.isDialog)
        {
            stateMachine.SwitchState(new PlayerMainState(stateMachine)); return;
        }
    }


}
