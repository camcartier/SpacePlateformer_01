using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showInstructionsLVL1 : MonoBehaviour
{
    [SerializeField] GameObject instructionsPanel;

    private void Start()
    {
        if (instructionsPanel != null)
        {
            if (instructionsPanel.activeInHierarchy)
            {
                instructionsPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instructionsPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            instructionsPanel.SetActive(false);
        }
    }
}
