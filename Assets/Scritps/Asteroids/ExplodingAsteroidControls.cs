using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplodingAsteroidControls : MonoBehaviour
{
    private bool isExploding;
    private bool isWaiting;
    public float timerCounter;
    public float durationCounter;
    public float delayBetweenTwoExplosions;
    public float durationOfExplosion;

    private bool isPlaying;

    public GameObject[] PSs = new GameObject[2];
    [SerializeField] Collider2D coll2D;

    private PlayerStateMachine playerStateMachine;

    // Start is called before the first frame update
    void Start()
    {
        isWaiting = true;
        foreach (GameObject Ps in PSs)
        { Ps.GetComponentInChildren<ParticleSystem>().Stop(); }

        playerStateMachine = GameObject.Find("Player").GetComponent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isExploding) 
        {
            //Debug.Log("exploding");

            if (coll2D != null) { coll2D.enabled = true; }


            if (!isPlaying)
            {
                foreach (GameObject Ps in PSs)
                { Ps.GetComponentInChildren<ParticleSystem>().Play(); }
                isPlaying = true;
            }

            

            durationCounter += Time.deltaTime;
            if(durationCounter > durationOfExplosion)
            {
                isExploding = false;
                isWaiting = true;
                durationCounter = 0f;
            }
        }

        if (isWaiting) 
        {
            //Debug.Log("waiting");

            if (coll2D != null) { coll2D.enabled = false; }
            

            if (isPlaying)
            {
                foreach (GameObject Ps in PSs)
                { Ps.GetComponentInChildren<ParticleSystem>().Stop(); }
                isPlaying= false;
            }


            timerCounter += Time.deltaTime;
            if(timerCounter > delayBetweenTwoExplosions)
            {
                isExploding = true;
                isWaiting = false;
                timerCounter = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //peut etre a deplacer
        if (collision.gameObject.CompareTag("Player"))
        {
            //gameObject.GetComponentInChildren<ParticleSystem>().Play();

            playerStateMachine.isHurt = true;

            /*if (!isExploding)
            {
                collision.gameObject.GetComponent<PlayerStateMachine>().isDead = true;
            }
            else { playerStateMachine.isHurt = true; }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("aie");
            //a faire dans le colliderReceiver
            //playerStateMachine.isHurt = true;
        }
    }
}
