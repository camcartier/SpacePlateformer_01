using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashingState : PlayerBaseState
{
    Vector2 movement = new Vector2();

    Vector2 dashDir;
    public Vector2 dashDir2;

    private float timerCounter;

    //public AnimationCurve dashCurve;
    //public float dashSpeed;

    public PlayerDashingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Debug.Log("is dashing");
        //stateMachine.InputReader.DashEvent += OnDash;

        stateMachine.dashResetTimer = 0f;

        timerCounter = 0;
        stateMachine.rb2D.velocity = Vector2.zero;

        dashDir = stateMachine.dashDirection.transform.position;


        stateMachine.isSliding = false;

        //addforce version
        //stateMachine.rb2D.AddForce(stateMachine.dashDirection2 * stateMachine.PlayerData.dashForce, ForceMode2D.Impulse);

        stateMachine.dashSound.Play();
        stateMachine.trailRenderer.emitting = true;
        //pas beau 
        //stateMachine.dashParticles.Play();
        //stateMachine.MainSpriteRenderer.color = new Color32(218,160,255,255);    
        stateMachine.MainSpriteRenderer.color = new Color32(236, 216, 255, 255);
    }

    public override void Exit()
    {
        //stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.MainSpriteRenderer.color = Color.white;
        //stateMachine.dashParticles.Stop();
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.isDead) { stateMachine.SwitchState(new PlayerDeathState(stateMachine)); return; }

        if (stateMachine.isDialog) {stateMachine.SwitchState(new PlayerDialogState(stateMachine)); return; }

        stateMachine.dashDirection2 = new Vector2(stateMachine.InputReader.AerialMovementValue.x,
                                                  stateMachine.InputReader.AerialMovementValue.y).normalized;



        movement = new Vector2(stateMachine.InputReader.GroundedMovementValue, 0);
        //if (movement.x < 0)
        //{ stateMachine.Visuals.transform.rotation = new Quaternion(0, 0, 0, 0); }
        //if (movement.x > 0)
        //{ stateMachine.Visuals.transform.rotation = new Quaternion(0, 180, 0, 0); }


        timerCounter += Time.deltaTime;

        float t = timerCounter / stateMachine.PlayerData.dashDuration;
        float multiplier = stateMachine.dashCurve.Evaluate(t);

        stateMachine.rb2D.velocity = stateMachine.dashDirection2 * multiplier * stateMachine.PlayerData.dashForce;

        if (timerCounter > stateMachine.PlayerData.dashDuration )
        {
            if (stateMachine.isGrounded && stateMachine.InputReader.Jump.ReadValue<float>() > 0)
            {
                stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
            }
            else
            {
                if (!stateMachine.isGrounded)
                {
                    stateMachine.SwitchState(new PlayerFallingState(stateMachine));
                }
                else { stateMachine.SwitchState(new PlayerMainState(stateMachine)); }
            }

               
        }

    }

    private void OnDash()
    {
        if (stateMachine.PlayerRessources.fuelCurrentAmount > 0)
        {
            stateMachine.SwitchState(new PlayerDashingState(stateMachine));

        }
    }

}
