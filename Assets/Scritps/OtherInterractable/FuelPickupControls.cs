using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuelPickupControls : MonoBehaviour
{
    public int fuelAmount;

    private PlayerStateMachine playerStateMachine;

    [SerializeField] GameObject fuelDialogPanel;

    private void Start()
    {
        playerStateMachine = GameObject.Find("Player").GetComponent<PlayerStateMachine>();

        fuelDialogPanel = GameObject.Find("Canvas_Fuel_Taken");
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

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name == "TestLevel1" && fuelDialogPanel != null)
        {
            fuelDialogPanel.SetActive(true);
        }

        
  
    }
}
