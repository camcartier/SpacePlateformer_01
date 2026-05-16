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

    public Collision2D aieCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("is grounded is" + stateMachine.isGrounded);
        Debug.Log("is sliding is" + isSliding);

        Collider2D hit = Physics2D.OverlapBox(GroundCheckPos.position,groundCheckSize,0,groundLayer);

        if (hit != null)
        {
            //Debug.Log(hit.name);
        }


        if (Physics2D.OverlapBox(GroundCheckPos.position, groundCheckSize, 0, groundLayer) && !stateMachine.isJumping)
        {
            //Debug.Log(hit.name);
            stateMachine.isGrounded = true ;
            stateMachine.canCoyoteJump = true;
        }
        else { stateMachine.isGrounded = false; }

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCollision = collision;

            Vector3 normal = collision.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                collisionColliders.Add(collision);

                aieCollider = collision;
            }
        }

        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    Vector3 normal = collision.GetContact(0).normal;
        //    if (normal == Vector3.up)
        //    {
        //        collisionColliders.Add(collision);

        //        //isGrounded = true;
        //    }
        //}


        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("aie");

            stateMachine.SwitchState(new PlayerHurtState(stateMachine));
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector2 bestNormal = Vector2.zero;

            for(int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;

                if(normal.y> bestNormal.y)
                {
                    bestNormal = normal;
                }
            }

            //Vector2 normal = collision.GetContact(0).normal;
            float slopAngle = Vector2.Angle(bestNormal, Vector2.up);

            if (slopAngle > 20)
            {
                stateMachine.isSliding = true;
            }
            else { stateMachine.isSliding = false; }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GroundCheckPos.position, groundCheckSize);
    }
}
