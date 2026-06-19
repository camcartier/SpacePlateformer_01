using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catControls : MonoBehaviour
{
    [SerializeField] GameObject txt;
    [SerializeField] AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txt.SetActive(true);
            //audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txt.SetActive(false);
        }
    }
}
