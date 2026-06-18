using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FallingPF_Controls : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D [] coll2D;
    private Rigidbody2D rigidbody2;

    private Vector2 initPosition;
    private Quaternion initQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        coll2D = GetComponentsInChildren<Collider2D>();
        rigidbody2 = GetComponent<Rigidbody2D>();

        initPosition = transform.position;
        initQuaternion = transform.rotation;
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
        
        foreach (Collider2D col in coll2D)
        {
            col.enabled = false;
        }
        

        yield return new WaitForSeconds(3f);
        spriteRenderer.color = Color.white;
        gameObject.transform.position = initPosition;
        gameObject.transform.rotation = initQuaternion;
        rigidbody2.constraints = RigidbodyConstraints2D.FreezeAll;
        rigidbody2.gravityScale = 1f;
        
        foreach (Collider2D col in coll2D)
        {
            col.enabled = true;
        }

        StopAllCoroutines();
    }
}
