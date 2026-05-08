using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerRessources playerRessources;
    
    void Start()
    {
        playerRessources.fuelCurrentAmount = playerRessources.fuelMaxAmount ;
        //Player.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
