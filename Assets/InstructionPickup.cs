using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionPickup : MonoBehaviour
{
    [SerializeField] GameObject instructionTXT;

    private bool hasBeenSeenOnce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
           if (collision.CompareTag("Player") && !hasBeenSeenOnce)
        {
            instructionTXT.SetActive(true);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instructionTXT.SetActive(false);
            hasBeenSeenOnce = true;
        }
    }
}
