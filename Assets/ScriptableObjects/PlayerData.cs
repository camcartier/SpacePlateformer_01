using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float groundedSpeed;
    public float aerialSpeed;
    public float fallingSpeed;
    public Vector2 jumpForce;
    public float gravityDownStrength;
    public float gravityDownDrag;
    public float gravityUpStrength;
    public float gravityUpDrag;
    public float maxGravityScale;

    public float jumpVelocity;
    public float maxJumpTime;

    public float boosterWait;
    public float boosterDuration;

    public float coyoteTime;
    public float boostStrength;
}

