using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActiveCanvas : MonoBehaviour
{
    [SerializeField] GameObject txtManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txtManager.SetActive(true);
        }
    }
}
