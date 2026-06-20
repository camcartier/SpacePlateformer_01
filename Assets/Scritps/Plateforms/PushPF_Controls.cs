using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class PushPF_Controls : MonoBehaviour
{
    public Vector2 pushForce;
    private Rigidbody2D rb2d;

    private bool hasMoved;

    private bool timerStarted;
    private float timer = 1f;
    private float timerCounter;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX |
                           RigidbodyConstraints2D.FreezePositionY |
                           RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            timerCounter += Time.deltaTime;
        }

        if (timerCounter> timer && Mathf.Abs(rb2d.velocity.x) < 0.01f)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX |
                               RigidbodyConstraints2D.FreezePositionY |
                               RigidbodyConstraints2D.FreezeRotation;
            timerStarted = false;
            timerCounter = 0f;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            timerStarted = true;

            rb2d.constraints = RigidbodyConstraints2D.FreezePositionY |
                   RigidbodyConstraints2D.FreezeRotation;

            if (collision.transform.position.x -  gameObject.transform.position.x > 0)
            {
                rb2d.AddForce(-pushForce, ForceMode2D.Impulse);
            }
            else
            {
                rb2d.AddForce(pushForce, ForceMode2D.Impulse);
            }

        }
    }
}
