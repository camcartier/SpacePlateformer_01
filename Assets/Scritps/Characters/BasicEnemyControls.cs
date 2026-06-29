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

    public float touchedPlayerStopTimer;
    private float touchedPlayerTimerCounter;
    private bool hasTouchedPlayer;

    [SerializeField] GameObject pointA;
    private Vector3 pointAWorld;
    [SerializeField] GameObject pointB;
    private Vector3 pointBWorld;
    private Vector3 targetPosition;
    public float waitDuration;

    public float walkSpeed;

    private bool coroutineIsRunning;

    [SerializeField] GameObject lootToSpawn;

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
        if (targetPosition.x - rb2D.transform.position.x > 0)
        {
            rb2D.transform.localScale = new Vector3(1,1,1);
        }
        else { rb2D.transform.localScale = new Vector3(-1, 1, 1); }


        if (numberOfHits > 1)
        {
            Instantiate(lootToSpawn, rb2D.transform.position, Quaternion.identity);
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
       

        if (hasTouchedPlayer)
        {
            rb2D.velocity = Vector3.zero;
            StopAllCoroutines();
            touchedPlayerTimerCounter += Time.deltaTime;
        }
        if (touchedPlayerTimerCounter > touchedPlayerStopTimer)
        {
            touchedPlayerTimerCounter = 0f;
            StartCoroutine(Patrol());
            hasTouchedPlayer = false;
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

        if (collision.gameObject.CompareTag("Player"))
        {
            hasTouchedPlayer  = true;   
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
