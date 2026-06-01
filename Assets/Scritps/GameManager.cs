using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerStateMachine playerStateMachine;
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerRessources playerRessources;

    [SerializeField] NarrationTXTManager narrationTXTManager;
    void Start()
    {
        playerRessources.fuelCurrentAmount = playerRessources.fuelMaxAmount ;
        //Player.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (playerStateMachine == null)
        {
            Debug.LogError("playerStateMachine est null");
            return;
        }*/

        
        if (SceneManager.GetActiveScene().name == "IntroDialogScene" && playerStateMachine != null && !narrationTXTManager.narrationIsOver)
        {
            playerStateMachine.isInHub = true;
            playerStateMachine.isDialog = true;
        }

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
