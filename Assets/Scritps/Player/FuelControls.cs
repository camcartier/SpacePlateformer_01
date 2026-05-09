using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelControls : MonoBehaviour
{
    private float keepFuelValue;
    [SerializeField] PlayerRessources playerRessources;
    [SerializeField] PlayerStateMachine stateMachine;

    public float refuelTimer;
    public float refuelTimerMax;
    
    public bool wasUsed;

    public float usedTimer;
    public float usedTimerCounter;

    void Start()
    {
        //initialisation des ressources
        playerRessources.fuelCurrentAmount = playerRessources.fuelMaxAmount;
        refuelTimer = stateMachine.PlayerRessources.refuelTimerDuration;
        stateMachine.PlayerRessources.currentRefuelSpeed = stateMachine.PlayerRessources.refuelSpeed;

        stateMachine.canFly = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerRessources.fuelCurrentAmount = Mathf.Clamp(playerRessources.fuelCurrentAmount,0, playerRessources.fuelMaxAmount);

        

        //si le joeur vole fuel goes down
        if (stateMachine.InputReader.Fly.ReadValue<float>() > 0 && stateMachine.canFly)
        {
            playerRessources.fuelCurrentAmount -= Time.deltaTime;
            stateMachine.PlayerRessources.currentRefuelSpeed = stateMachine.PlayerRessources.refuelSpeed;
            usedTimerCounter = 0;
        }


        //fuel is depleted, player can't fly
        if (playerRessources.fuelCurrentAmount <= 0)
        {
            stateMachine.canFly = false;

        }
        

        if (playerRessources.fuelCurrentAmount >0)
        {
          stateMachine.canFly = true;
            
        }

        //si le player ne peut voler et n'est pas en train de voler, alors le timer descend
        if (!stateMachine.canFly && !stateMachine.isFlying && usedTimerCounter >= usedTimer)
        {
            refuelTimer -= Time.deltaTime;
        }

        //si le timer est a 0 et le joueur n'est pas en train de voler, le fuel remonte
        if (refuelTimer <= 0 && !stateMachine.isFlying && usedTimerCounter >= usedTimer && playerRessources.fuelCurrentAmount< playerRessources.fuelMaxAmount)
        {
            stateMachine.PlayerRessources.currentRefuelSpeed += Time.deltaTime * stateMachine.PlayerRessources.refuelAcceleration;
            playerRessources.fuelCurrentAmount += stateMachine.PlayerRessources.currentRefuelSpeed * Time.deltaTime ;
            //refuelTimer = stateMachine.PlayerRessources.refuelTimerDuration;
        }


        
        if (usedTimerCounter < usedTimer)
        {
            usedTimerCounter+= Time.deltaTime;
        }



         
    }
}
