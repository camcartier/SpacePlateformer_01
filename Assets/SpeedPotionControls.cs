using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotionControls : MonoBehaviour
{
    [SerializeField] SpeedModifiers speedModifier;

    public float timerCounter;
    public float timer;

    private bool isSpeeding;

    private SpriteRenderer spriteRenderer;
    private Collider2D coll2D;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        coll2D = GetComponentInChildren<Collider2D>();
        
        speedModifier.speedValue = 1;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isSpeeding);

        if (isSpeeding)
        {
            Debug.Log("speeding");
            timerCounter += Time.deltaTime;
        }
        

        if (timerCounter > timer)
        {
            speedModifier.speedValue = 1;
            speedModifier.verticalSpeedValue = 0f;
            timerCounter = 0f;
            isSpeeding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSpeeding = true;
            speedModifier.speedValue = 1.5f;
            speedModifier.verticalSpeedValue = 1f;

            spriteRenderer.enabled = false;
            coll2D.enabled = false;
        }
    }
}
