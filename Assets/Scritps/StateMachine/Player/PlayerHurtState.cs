using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    private float timerCounter = 0.5f;
    private float timer;

    private Vector2 aieCollision;
    private Vector2 direction;
    public PlayerHurtState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        aieCollision = new Vector2(stateMachine.ColliderReceiver.aieCollider.transform.position.x, stateMachine.ColliderReceiver.aieCollider.transform.position.y);
        direction = new Vector2(stateMachine.transform.position.x - aieCollision.x,
                                                  stateMachine.transform.position.y - aieCollision.y).normalized;

        stateMachine.rb2D.AddForce(new Vector2(direction.x * 75,
                                                  direction.y * 75), ForceMode2D.Impulse);

        stateMachine.rb2D.drag = 6f;
    }

    public override void Exit()
    {
        stateMachine.rb2D.drag = 1f;
    }

    public override void Tick(float deltaTime)
    {
        timerCounter -= Time.deltaTime;

        if (timerCounter <= 0f) { stateMachine.SwitchState(new PlayerFallingState(stateMachine)); }
    }


}
