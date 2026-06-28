//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
 

    //Movement
    [field: SerializeField] public Animator Animator { get; set; }
    [field: SerializeField] public Rigidbody2D rb2D { get; private set; }

    [field: SerializeField] public SpriteRenderer MainSpriteRenderer { get; private set; }

    

    [field: SerializeField] public ColliderReceiver ColliderReceiver { get; private set; }
    [field: SerializeField] public Collision2D aieCollider { get; set; }
    

    [field: SerializeField] public bool canJump { get; set; }
    [field: SerializeField] public bool isJumping { get; set; }
    [field: SerializeField] public bool wantsToJump { get; set; }
    [field: SerializeField] public bool canCoyoteJump { get; set; }

    public bool previousStateWasJump;

    [field: SerializeField] public bool isGrounded { get; set; }
    [field: SerializeField] public bool jumpIsOver { get; set; }



    [field: SerializeField] public bool canDash { get; set; }
    [field: SerializeField] public float dashResetTimer { get; set; }
    [field: SerializeField] public DashResetTImer dashResetTimerScript { get; set; }
    [field: SerializeField] public GameObject dashDirection { get; set; }
    [field: SerializeField] public Vector2 dashDirection2 { get; set; }
    [field: SerializeField] public AnimationCurve dashCurve { get; set; }


    [field: SerializeField] public GameObject bulletStartPoint { get; set; }
    [field: SerializeField] public GameObject bullet { get; set; }
    [field: SerializeField] public Instantiator instantiator { get; set; }
    //[field: SerializeField] public Light2D light2D { get; set; }


    [field: SerializeField] public bool isOnASLope { get; set; }
    [field: SerializeField] public Vector2 slopeDirection { get; set; }
    [field: SerializeField] public bool isSliding { get; set; }

    [field: SerializeField] public bool canFly { get; set; }
    [field: SerializeField] public bool isFlying { get; set; }

    [field: SerializeField] public bool canShoot { get; set; }

    [field: SerializeField] public bool isBoosted { get; set; }
    [field: SerializeField] public GameObject currentBooster { get; set; }
    [field: SerializeField] public AnimationCurve boostCurve { get; set; }


    [field: SerializeField] public PlayerData PlayerData { get; private set; }
    [field: SerializeField] public SpeedModifiers SpeedModifier { get; private set; }
    [field: SerializeField] public PlayerRessources PlayerRessources { get; private set; }


    [field: SerializeField] public ParticleSystem flyingParticles { get; set; }
    [field: SerializeField] public ParticleSystem boostedParticles { get; set; }
    [field: SerializeField] public ParticleSystem jumpParticles { get; set; }
    [field: SerializeField] public ParticleSystem dashParticles { get; set; }
    [field: SerializeField] public TrailRenderer trailRenderer { get; set; }
    [field: SerializeField] public GameObject Visuals{ get; private set; }


    //stun and knockback
    [field: SerializeField] public Vector2 knockbackDirection { get; set; }
    [field: SerializeField] public int knockbackForce { get; private set; }




    //so we don't get back in hurt state when in contact and dead
    [field: SerializeField] public bool isDead { get; set; }
    [field: SerializeField] public bool isHurt { get; set; }

    //pour qu'on se fasse pas marave comme une victime
    [field: SerializeField] public bool isInvulnerable { get; set; }


    [field: SerializeField] public bool isDialog { get; set; }

    [field: SerializeField] public bool isInHub { get; set; }
    //Use
    //[field: SerializeField] public bool isUsing { get; set; }


    [field: SerializeField] public GameManager gameManager { get; set; }

    [field: SerializeField] public AudioSource stepSound { get; private set; }
    [field: SerializeField] public AudioSource jumpSound { get; private set; }
    [field: SerializeField] public AudioSource dashSound { get; private set; }
    [field: SerializeField] public AudioSource boostSound { get; private set; }
    [field: SerializeField] public AudioSource landingSound { get; private set; }
    [field: SerializeField] public AudioSource jetpackSound { get; private set; }
    [field: SerializeField] public AudioSource blasterSound { get; private set; }

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