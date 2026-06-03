using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTitleScene : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject loadPanel;

    void Start()
    {
        
    }


    void Update()
    {
        
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnOffSettings()
    {
        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
        }
        else { settingsPanel.SetActive(true); }
    }

    public void OnOffLoading()
    {
        if (loadPanel.activeInHierarchy)
        {
            loadPanel.SetActive(false);
        }
        else { loadPanel.SetActive(true); }
    }
}
