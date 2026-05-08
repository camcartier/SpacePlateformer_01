using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoostedState : PlayerBaseState
{
    private float TimerCounter;
    private float changeStateTimer = 0.75f;
    private float changeStateCounter;
    private bool hasBeenBoosted;

    //test
    public PointA pointA;
    public PointB pointB;
    //[SerializeField] private AnimationCurve curve;
    //private float duration = 1f;

    private Vector2 playerInfluence;

    public PlayerBoostedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("enter boost");
        TimerCounter = 0f;

        stateMachine.boostedParticles.Play();

        pointA = stateMachine.currentBooster.GetComponentInChildren<PointA>();
        pointB = stateMachine.currentBooster.GetComponentInChildren<PointB>();

        stateMachine.rb2D.gravityScale = 5f;
        //stateMachine.rb2D.velocity = Vector2.zero;
        //stateMachine.rb2D.velocity = stateMachine.currentBooster.transform.up * stateMachine.PlayerData.boostStrength;

        //stateMachine.rb2D.AddForce(new Vector2(0, 200), ForceMode2D.Impulse);

        hasBeenBoosted = true;

        stateMachine.previousStateWasJump = true;
    }

    public override void Exit()
    {
        stateMachine.boostedParticles.Stop();

        hasBeenBoosted = false;
    }



    public override void Tick(float deltaTime)
    {

        //stateMachine.rb2D.velocity = new Vector2(stateMachine.InputReader.GroundedMovementValue* stateMachine.PlayerData.aerialSpeed, stateMachine.rb2D.velocity.y);


        //trop rigide
        TimerCounter += Time.deltaTime;

        float t = TimerCounter / stateMachine.PlayerData.boosterDuration;

        t = Mathf.Clamp01(t);

        float curvedT = stateMachine.boostCurve.Evaluate(t);



        stateMachine.rb2D.transform.position = Vector2.Lerp(pointA.gameObject.transform.position,
                                                            pointB.gameObject.transform.position,
                                                           curvedT);


        if (t>=1) { stateMachine.SwitchState(new PlayerFallingState(stateMachine)); }




        //if (stateMachine.rb2D.velocity.y <= 0)
        //{
        //    stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        //}

    }

}
