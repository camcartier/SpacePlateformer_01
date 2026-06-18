using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControls : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public Vector2 force;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        rb2D.AddForce(force, ForceMode2D.Impulse); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
