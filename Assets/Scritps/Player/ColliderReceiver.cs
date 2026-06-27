using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderReceiver : MonoBehaviour
{
    [SerializeField] PlayerStateMachine stateMachine;

    private float TimerCounter;
    public bool isGrounded;
    public bool isSliding;

    public List<Collision2D> collisionColliders = new List<Collision2D>() ;
    public Collision2D groundCollision;

    [Header("Groundcheck")]
    public Transform GroundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f ,0.5f);
    public LayerMask groundLayer;

    //pour la state machine
    public Collision2D aieCollider;

    public float slopeSlowingFactor;
    
    [SerializeField] ParticleSystem landingPS;
    [SerializeField] GameObject instantiatePos;
    private ParticleSystem instantiatedPS;
    [SerializeField] AudioSource landingSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("is grounded is" + stateMachine.isGrounded);
        //Debug.Log("is sliding is" + isSliding);



        Collider2D hit = Physics2D.OverlapBox(GroundCheckPos.position,groundCheckSize,0,groundLayer);

        RaycastHit2D raycasthit = Physics2D.Raycast(GroundCheckPos.position, Vector2.down, 3f, groundLayer);

        if (hit != null)
        {
            //Debug.Log(hit.name);
        }
        if (raycasthit)
        {
            //Debug.Log("Hit : " + raycasthit.collider.name);
        }
        else
        {
            //Debug.Log("No Hit");
        }

            Debug.DrawRay(GroundCheckPos.position,
                           Vector2.down * 20f,
                           Color.white);

        if (Physics2D.OverlapBox(GroundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            //Debug.Log(hit.name);
            stateMachine.isGrounded = true ;

            stateMachine.canCoyoteJump = true;
        }
        else { stateMachine.isGrounded = false; landingSound.Play(); }

        if (raycasthit)
        {
            float slopeAngle = Vector2.Angle(raycasthit.normal, Vector2.up);
            float slopeNormal = raycasthit.normal.x;
            
            stateMachine.slopeDirection = new Vector2(raycasthit.normal.y, -raycasthit.normal.x).normalized;

            //Debug.Log(raycasthit.collider.name);
            //Debug.Log(raycasthit.normal);
            //Debug.Log(slopeAngle);


            if (slopeAngle > 5)
            {
                stateMachine.isOnASLope = true ;
            }
            else { stateMachine.isOnASLope = false; }


            if (slopeAngle >50)
            {
                stateMachine.isSliding = true ;
                //old
                slopeSlowingFactor = -16f;
            }
            else
            {
                stateMachine.isSliding = false ;
            }



            //if (slopeAngle > 80)
            //{ stateMachine.isSliding = true; }
            //else
            //{ stateMachine.isSliding = false; }

        }

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           
            instantiatedPS = Instantiate(landingPS, instantiatePos.transform.position, Quaternion.identity) ;
            instantiatedPS.Play();

            groundCollision = collision;

            Vector3 normal = collision.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                collisionColliders.Add(collision);

                aieCollider = collision;
            }

            //landingSound.Play();
        }

        if (collision.gameObject.CompareTag("Metal"))
        {

            groundCollision = collision;

            Vector3 normal = collision.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                collisionColliders.Add(collision);

                aieCollider = collision;
            }

            //landingSound.Play();

        }


        if (collision.gameObject.CompareTag("Asteroid") && !stateMachine.isBoosted)
        {
            Debug.Log("dead");

            stateMachine.aieCollider = collision;

            stateMachine.isDead = true ;
        }


        if (collision.gameObject.CompareTag("CanDamage") && !stateMachine.isBoosted)
        {
            Debug.Log("aie");

            stateMachine.aieCollider = collision;

            //deja dans l'autre script pour l'instant
            //stateMachine.isHurt = true ;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collisionColliders.Remove(collision);
            //isGrounded = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            //Debug.Log("star");

            stateMachine.isBoosted = true;
            stateMachine.currentBooster = collision.gameObject;
        } 

        
    }





    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    Vector2 bestNormal = Vector2.zero;

        //    for(int i = 0; i < collision.contactCount; i++)
        //    {
        //        Vector2 normal = collision.GetContact(i).normal;

        //        if(normal.y> bestNormal.y)
        //        {
        //            bestNormal = normal;
        //        }
        //    }

        //    //Vector2 normal = collision.GetContact(0).normal;
        //    float slopAngle = Vector2.Angle(bestNormal, Vector2.up);

        //    if (slopAngle > 20)
        //    {
        //        stateMachine.isSliding = true;
        //    }
        //    else { stateMachine.isSliding = false; }
        //}
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GroundCheckPos.position, groundCheckSize);
    }

    private void OnDrawGizmos()
    {
        if (GroundCheckPos == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(GroundCheckPos.position,
                        GroundCheckPos.position + Vector3.down * 3f);
    }
}
