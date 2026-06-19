using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletControls : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public Vector2 force;

    [SerializeField] GameObject impactPS;
    private GameObject instatiatedPS;

    private GameObject Player;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");

        if (Player.transform.position.x - gameObject.transform.position.x > 0)
        {
            rb2D.AddForce(-force, ForceMode2D.Impulse);
        }
        else
        {
            rb2D.AddForce(force, ForceMode2D.Impulse);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        instatiatedPS = Instantiate(impactPS, collision.otherCollider.transform.position, Quaternion.identity);
        instatiatedPS.SetActive(true);
        instatiatedPS.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(destroyGameObject());
    }

    IEnumerator destroyGameObject()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
