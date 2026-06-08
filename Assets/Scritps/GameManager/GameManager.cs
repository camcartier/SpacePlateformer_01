using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerStateMachine playerStateMachine;
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerRessources playerRessources;
    [SerializeField] FuelControls fuelControls;

    [SerializeField] NarrationTXTManager narrationTXTManager;
    private InfoHolder infoHolder;

    public bool isPaused;
    [SerializeField] GameObject pausePanel;

    private void Awake()
    {
        infoHolder = InfoHolder.Instance;
    }
    void Start()
    {
        playerRessources.fuelCurrentAmount = playerRessources.fuelMaxAmount ;
        //Player.GetComponentInChildren<ParticleSystem>().Stop();

        if (pausePanel != null)
        {
            pausePanel.SetActive(false); 
        }
        if (isPaused ) {UnpauseGame(); }

        if (SceneManager.GetActiveScene().name == "TestLevel1")
        {
            playerRessources.fuelCurrentAmount = 0f;
            fuelControls.canRefuel = false;

        }
        else
        {
            fuelControls.canRefuel = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (narrationTXTManager != null)
        {
            if (!infoHolder.dialogHasHappened)
            {
                if (SceneManager.GetActiveScene().name == "IntroDialogScene" && playerStateMachine != null && !narrationTXTManager.narrationIsOver)
                {
                    playerStateMachine.isInHub = true;
                    playerStateMachine.isDialog = true;
                }

                if (narrationTXTManager.dialogIsAtStart && !narrationTXTManager.narrationIsOver)
                {
                    playerStateMachine.isDialog = true;
                }
                else
                {
                    playerStateMachine.isDialog = false;
                }
            }


        }


        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else { UnpauseGame(); }

        }

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pausePanel.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel.SetActive(false);
    }

    public void GetToMainMenu()
    {
        SceneManager.LoadScene("MainTitleScene");
    }

    public void RestartLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UnpauseGame();
    }
}
