using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickupControls : MonoBehaviour
{
    public int fuelAmount;

    private PlayerStateMachine playerStateMachine;

    private void Start()
    {
        playerStateMachine = GameObject.Find("Player").GetComponent<PlayerStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("player gets fuel");

            if (playerStateMachine != null)
            {
                
                playerStateMachine.PlayerRessources.fuelCurrentAmount += fuelAmount;
            }
            else { Debug.Log("pas trouvé"); }
           
        }
    }
}
