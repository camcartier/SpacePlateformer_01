using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAsteroidsControls : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    private Vector3 pointAWorld;
    [SerializeField] GameObject pointB;
    private Vector3 pointBWorld;
    [SerializeField] GameObject asteroid;
    [SerializeField] float speed;
    [SerializeField] float waitTime;

    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        pointAWorld = pointA.transform.position;
        pointBWorld = pointB.transform.position;

        pointA.SetActive(false);
        pointB.SetActive(false);

        //transform.position = pointAWorld;
        targetPosition = pointB.transform.position;


        StartCoroutine(MoveAsteroid());
    }

    IEnumerator MoveAsteroid()
    {
        while (true)
        {
            while ((targetPosition - transform.position).sqrMagnitude > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                                                                  targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            targetPosition =
                targetPosition == pointAWorld
                ? pointBWorld
                : pointAWorld;

            //just a fancy way of doing if i guess and not working
            //targetPosition = targetPosition == pointA.transform.position
            //  ? pointB.transform.position : pointA.transform.position;

            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();

            collision.gameObject.GetComponent<PlayerStateMachine>().isDead = true;
        }
    }
}
