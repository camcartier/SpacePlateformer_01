using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelControls : MonoBehaviour
{
    private float keepFuelValue;
    [SerializeField] PlayerRessources playerRessources;
    [SerializeField] PlayerStateMachine stateMachine;

    public float refuelTimer;
    public bool isDepleted;

    void Start()
    {
        playerRessources.fuelCurrentAmount = playerRessources.fuelMaxAmount;

        stateMachine.canFly = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && playerRessources.fuelCurrentAmount > 0f)
        {
            playerRessources.fuelCurrentAmount -= Time.deltaTime;
        }

        if (playerRessources.fuelCurrentAmount <= 0)
        {
            stateMachine.canFly = false;
        }
        
        if (playerRessources.fuelCurrentAmount >= playerRessources.fuelMaxAmount)
        {
          stateMachine.canFly = true;
            
        }

        if (!stateMachine.canFly )
        {
            refuelTimer -= Time.deltaTime;
        }

        if (refuelTimer <= 0 && !stateMachine.canFly )
        {
            playerRessources.fuelCurrentAmount += Time.deltaTime * 1.02f;
        }
        else { return; }

        
        
         
    }
}
