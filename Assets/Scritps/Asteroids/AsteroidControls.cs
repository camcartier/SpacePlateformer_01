using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControls : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private int chosenSpriteNumber;

    public SpriteRenderer[] arrayOfSprites = new SpriteRenderer[5];

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();

        chosenSpriteNumber = Random.Range(0, arrayOfSprites.Length);

        for (int i = 0; i < arrayOfSprites.Length; i++)
        {
            if (i == chosenSpriteNumber)
            {
                arrayOfSprites[i].enabled = true;
            }
            else
            {
                arrayOfSprites[i].enabled = false;
            }
        }

        spriteRenderer.sprite = arrayOfSprites[chosenSpriteNumber].sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
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
