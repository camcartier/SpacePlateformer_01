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
    [SerializeField] CheckPointTracker checkPointTracker;
    //[SerializeField] CheckPointControls checkPointControls;
    public int checkPointNumber;

    [SerializeField] TXTManagerStart TXTManagerStart;
    private InfoHolder infoHolder;

    public bool isPaused;
    [SerializeField] GameObject pausePanel;

    public bool playerIsDead;

    [SerializeField] GameObject fuelPickup;
    [SerializeField] GameObject fuelPosition;

    private void Awake()
    {
        infoHolder = InfoHolder.Instance;
    }
    void Start()
    {
        playerIsDead = false;

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
        Debug.Log("game Manager says isDialog is " + playerStateMachine.isDialog);

        if (playerStateMachine.rb2D.velocity.y > 0)
        {
            //Debug.Log(playerStateMachine.rb2D.velocity);
        }
        


        if (playerIsDead)
        {
            if (checkPointTracker.currentCheckpoint == null) { RestartLVL(); }
            else { RestartAtCheckpoint();  }
            
        }

        



        if (TXTManagerStart != null && infoHolder != null)
        {
            if (!infoHolder.dialogHasHappened)
            {
                if (SceneManager.GetActiveScene().name == "IntroDialogScene" && playerStateMachine != null && !TXTManagerStart.narrationIsOver)
                {
                    playerStateMachine.isInHub = true;
                    playerStateMachine.isDialog = true;
                }

                if (TXTManagerStart.dialogIsAtStart && !TXTManagerStart.narrationIsOver)
                {
                    playerStateMachine.isDialog = true;
                }
                /*else
                {
                    playerStateMachine.isDialog = false;
                }*/
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

    public void RestartAtCheckpoint()
    {
        Debug.Log("Respawn");
        Player.transform.position = checkPointTracker.currentCheckpoint.transform.position;
        playerIsDead = false;
        Player.GetComponent<PlayerStateMachine>().isDead = false;
        
        if (checkPointTracker.checkpointNumber == 2)
        {
            playerRessources.fuelCurrentAmount = 0;
            Instantiate(fuelPickup, fuelPosition.transform.position, Quaternion.identity);
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
