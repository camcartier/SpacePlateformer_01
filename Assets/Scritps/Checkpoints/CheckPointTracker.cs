using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointTracker : MonoBehaviour
{
    public GameObject currentCheckpoint;
    public int checkpointNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            currentCheckpoint = collision.gameObject;

            if (collision.gameObject.TryGetComponent<CheckPointNumber>(out CheckPointNumber cpn))
            {
                checkpointNumber = collision.gameObject.GetComponent<CheckPointNumber>().number;
            }
        }
    }
}
