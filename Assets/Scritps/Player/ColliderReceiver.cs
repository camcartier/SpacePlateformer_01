using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReceiver : MonoBehaviour
{
    [SerializeField] PlayerStateMachine stateMachine;

    private float TimerCounter;
    public bool isGrounded;

    public List<Collision2D> collisionColliders = new List<Collision2D>() ;

    [Header("Groundcheck")]
    public Transform GroundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f ,0.5f);
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionColliders.Count > 0)
        {
        //isGrounded = true;
        }

        if(collisionColliders.Count <= 0)
        {
            //isGrounded = false;
        }
        //else { isGrounded = false; }

        if (Physics2D.OverlapBox(GroundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            isGrounded = true ;
        }
        else { isGrounded = false; }
        //isGrounded = false;
    }

    //public bool isGrounded()
    //{
    //    if (Physics2D.OverlapBox(GroundCheckPos.position, groundCheckSize, 0, groundLayer))
    //    {
    //        return true;
    //    }
    //    return false;
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = collision.GetContact(0).normal;
            if(normal == Vector3.up)
            {
                collisionColliders.Add(collision);

                //isGrounded = true;
            }
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("aie");
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GroundCheckPos.position, groundCheckSize);
    }
}
