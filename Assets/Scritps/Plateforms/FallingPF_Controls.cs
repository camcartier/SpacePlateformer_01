using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FallingPF_Controls : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D coll2D;
    private Rigidbody2D rigidbody2;

    private Vector2 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        coll2D = GetComponentInChildren<Collider2D>();
        rigidbody2 = GetComponent<Rigidbody2D>();

        initPosition = transform.position;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlateformFalls());
        }
    }

    public IEnumerator PlateformFalls()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);

        rigidbody2.constraints = RigidbodyConstraints2D.None;
        rigidbody2.gravityScale = 8f;
        
        yield return new WaitForSeconds(0.5f);
        coll2D.enabled = false;

        yield return new WaitForSeconds(3f);
        spriteRenderer.color = Color.white;
        gameObject.transform.position = initPosition;
        rigidbody2.constraints = RigidbodyConstraints2D.FreezeAll;
        rigidbody2.gravityScale = 1f;
        coll2D.enabled = true;
        StopAllCoroutines();
    }
}
