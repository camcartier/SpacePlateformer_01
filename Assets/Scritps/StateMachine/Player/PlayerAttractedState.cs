using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttractedState : PlayerBaseState
{
    private float timer = 1f;
    private float timerCounter;

    public PlayerAttractedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("attracted");
        stateMachine.rb2D.velocity = Vector2.zero;
        stateMachine.rb2D.gravityScale = 0f;
    }

    public override void Exit()
    {
        timerCounter = 0f;
        stateMachine.isBoosted= false;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.transform.position != stateMachine.currentBooster.transform.position)
        {
           stateMachine.rb2D.MovePosition(stateMachine.currentBooster.transform.position);
            stateMachine.rb2D.rotation = stateMachine.currentBooster.transform.eulerAngles.z;
            //compliqué
           stateMachine.rb2D.velocity = Vector2.zero;
        }
        
        if (timerCounter < timer)
        {
            timerCounter+=  Time.deltaTime;
        }
        else
        {
            stateMachine.SwitchState(new PlayerBoostedState(stateMachine));
        }
             

    }

}
