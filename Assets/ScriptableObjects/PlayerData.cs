using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [Header("Move Speed")]
    public float groundedSpeed;
    public float aerialSpeed;
    public float XaerialSpeed;
    public float YaerialSpeed;
    public float fallingSpeed;

    [Header("Gravity")]
    public float gravityDownStrength;
    public float gravityDownDrag;
    public float gravityUpStrength;
    public float gravityUpDrag;
    public float maxGravityScale;

    [Header("Fall")]
    public Vector2 fallVector;
    public float fallForce;

    [Header("Sliding")]
    public float currentSlideValue;
    public float slideValueStartPoint;
    public float slideValueEndPoint;

    [Header("Dashing")]
    public float dashDuration;
    public float dashForce;

    [Header("Jump Stats")]
    public Vector2 jumpForce;
    public float jumpVelocity;
    public float maxJumpTime;
    public float coyoteTime;

    [Header("Boost")]
    public float boosterWait;
    public float boosterDuration;
    public float boostStrength;

    [Header("Decceleration")]
    public float flyDecceleration;
    public float flyMaxAcceleration;

}

