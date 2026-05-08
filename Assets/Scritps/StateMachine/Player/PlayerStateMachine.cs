//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
 

    //Movement
    //[field: SerializeField] public Animator Animator { get; set; }
    [field: SerializeField] public Rigidbody2D rb2D { get; private set; }

    //[field: SerializeField] public SpriteRenderer MainSpriteRenderer { get; private set; }

    


    [field: SerializeField] public ColliderReceiver ColliderReceiver { get; private set; }

    [field: SerializeField] public bool canJump { get; set; }
    [field: SerializeField] public bool isJumping { get; set; }
    [field: SerializeField] public bool canCoyoteJump { get; set; }

    [field: SerializeField] public bool isGrounded { get; set; }
    [field: SerializeField] public bool jumpIsOver { get; set; }


    [field: SerializeField] public bool isBoosted { get; set; }
    [field: SerializeField] public GameObject currentBooster { get; set; }
    [field: SerializeField] public AnimationCurve boostCurve { get; set; }


    [field: SerializeField] public PlayerData PlayerData { get; private set; }
    [field: SerializeField] public PlayerRessources PlayerRessources { get; private set; }


    [field: SerializeField] public ParticleSystem flyingParticles { get; set; }
    [field: SerializeField] public ParticleSystem boostedParticles { get; set; }
    [field: SerializeField] public GameObject Visuals{ get; private set; }


    //stun and knockback
    [field: SerializeField] public Vector2 knockbackDirection { get; set; }
    [field: SerializeField] public int knockbackForce { get; private set; }




    //so we don't get back in hurt state when in contact and dead
    [field: SerializeField] public bool isDead { get; set; }
    [field: SerializeField] public bool isHurt { get; set; }

    //pour qu'on se fasse pas marave comme une victime
    [field: SerializeField] public bool isInvulnerable { get; set; }



    //Use
    //[field: SerializeField] public bool isUsing { get; set; }


    //Dash
    //[field: SerializeField] public bool canDash { get; set; }
    //[field: SerializeField] public bool isDashing { get; set; }
    //[field: SerializeField] public float dashDuration { get; private set; }
    //[field: SerializeField] public Vector2 lastMovementDirection { get; set; }




    //General
    //public GameManager GameManager { get; set; }
    //public Transform MainCameraTransform { get; private set; }
    //[field: SerializeField] public CameraCoroutines CameraCoroutines { get; private set; }
    //[field: SerializeField] public CameraData CameraData { get; set; }

    //public CinemachineVirtualCamera CinemachineVirtualCamera { get; set; }


    void Start()
    {
        //GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //MainCameraTransform = Camera.main.transform;
        //CinemachineVirtualCamera = GameObject.Find("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();

        //if (Animator == null)
        //{
        //    Animator = gameObject.GetComponentInChildren<Animator>();
        //}

        SwitchState(new PlayerMainState(this));

        boostedParticles.Stop();
        flyingParticles.Stop();
    }



}