using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

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
        //Debug.Log("enter boost");
        TimerCounter = 0f;

        stateMachine.boostedParticles.Play();


        pointA = stateMachine.currentBooster.GetComponentInChildren<PointA>();
        pointB = stateMachine.currentBooster.GetComponentInChildren<PointB>();

        stateMachine.rb2D.gravityScale = 5f;

        hasBeenBoosted = true;
        stateMachine.isBoosted = true;  

        stateMachine.previousStateWasJump = true;
        stateMachine.isSliding = false;

        Vector2  direction = ((Vector2)pointB.gameObject.transform.position - (Vector2)pointA.gameObject.transform.position).normalized;

        stateMachine.rb2D.AddForce(direction * stateMachine.PlayerData.boostAddForceStrenght, ForceMode2D.Impulse);

        stateMachine.trailRenderer.emitting = true;
    }

    public override void Exit()
    {
        stateMachine.boostedParticles.Stop();

        hasBeenBoosted = false;
        stateMachine.isBoosted = false;
    }



    public override void Tick(float deltaTime)
    {

        //stateMachine.rb2D.velocity = new Vector2(stateMachine.InputReader.GroundedMovementValue* stateMachine.PlayerData.aerialSpeed, stateMachine.rb2D.velocity.y);


        //trop rigide
        TimerCounter += Time.deltaTime;

        float t = TimerCounter / stateMachine.PlayerData.boosterDuration;

        t = Mathf.Clamp01(t);

        /*
        float curvedT = stateMachine.boostCurve.Evaluate(t);


        Vector2 lerpResult = Vector2.Lerp(pointA.gameObject.transform.position,
                                                            pointB.gameObject.transform.position,
                                                           curvedT);

        Vector2 inputResult = stateMachine.InputReader.AerialMovementValue;


        stateMachine.rb2D.transform.position = lerpResult + inputResult;
        */

        
        Vector2 velocity = stateMachine.rb2D.velocity;


        //float targetHorizontalSpeed = stateMachine.InputReader.AerialMovementValue.x * 10f;
        //une valeur arbitraire. la force de la deviation
        float deviationStrenght = 100;

        /*
        if((Mathf.Abs(stateMachine.InputReader.AerialMovementValue.x) > 0.1f))
        {
            velocity.x = Mathf.Lerp(velocity.x, targetHorizontalSpeed, deviationStrenght * Time.deltaTime);
        }*/

        velocity.x += stateMachine.InputReader.AerialMovementValue.x * stateMachine.PlayerData.maxBoostDeviationXStrenght * Time.deltaTime; 
        
        //ouais mais alors ça nique les diagonales?
        //velocity.x = Mathf.Clamp(velocity.x, -stateMachine.PlayerData.maxBoostDeviationXStrenght, stateMachine.PlayerData.maxBoostDeviationXStrenght);

        stateMachine.rb2D.velocity = velocity;


        if (t>=1) { stateMachine.SwitchState(new PlayerFallingState(stateMachine)); return; }




        //if (stateMachine.rb2D.velocity.y <= 0)
        //{
        //    stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        //}

    }

}
