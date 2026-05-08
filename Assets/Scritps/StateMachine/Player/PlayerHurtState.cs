using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    private float timerCounter;
    private float timer;
    public PlayerHurtState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        timerCounter -= Time.deltaTime;
    }


}
