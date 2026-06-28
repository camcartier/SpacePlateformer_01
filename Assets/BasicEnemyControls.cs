using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicEnemyControls : MonoBehaviour
{
    private int numberOfHits;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    public float stunDuration;
    private float stunTimerCounter;

    [SerializeField] GameObject pointA;
    private Vector3 pointAWorld;
    [SerializeField] GameObject pointB;
    private Vector3 pointBWorld;
    private Vector3 targetPosition;
    public float waitDuration;

    public float walkSpeed;

    private bool coroutineIsRunning;

    // Start is called before the first frame update
    void Start()
    {   
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        pointAWorld = pointA.transform.position;
        pointBWorld = pointB.transform.position;

        pointA.SetActive(false);
        pointB.SetActive(false);

        targetPosition = pointB.transform.position;

        StartCoroutine(Patrol());
        coroutineIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {


        if (numberOfHits > 1) 
        {
            Destroy(gameObject);
        }

        if (stunTimerCounter < stunDuration) 
        {
            stunTimerCounter += Time.deltaTime;
        }
        else
        {
            stunTimerCounter = 0f;
            spriteRenderer.color = Color.white;

            if (!coroutineIsRunning)
            {
                StartCoroutine(Patrol());
                coroutineIsRunning = true;
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            rb2D.velocity = Vector2.zero;
            StopAllCoroutines();
            coroutineIsRunning = false;
            spriteRenderer.color = Color.red;
            numberOfHits++;
            
        }
    }

    public IEnumerator Patrol()
    {
        while (true)
        {
            while ((targetPosition - transform.position).sqrMagnitude > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                                                                  targetPosition, walkSpeed * Time.deltaTime);
                yield return null;
            }

            targetPosition =
                targetPosition == pointAWorld
                ? pointBWorld
                : pointAWorld;


            yield return new WaitForSeconds(waitDuration);
        }

    }
}
