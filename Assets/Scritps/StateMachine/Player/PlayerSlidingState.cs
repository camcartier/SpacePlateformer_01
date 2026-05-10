using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlidingState : PlayerBaseState
{
    public PlayerSlidingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("sliding");
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {

    }


}
