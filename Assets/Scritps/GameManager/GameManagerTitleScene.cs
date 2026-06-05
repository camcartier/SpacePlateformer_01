using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerTitleScene : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject loadPanel;

    [SerializeField] GameObject blackScreen;
    public float timeBeforeLoadScene;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource clickSound;

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

    public void StartCoroutineBlackScreen()
    {
        StartCoroutine(ActionBeforeNextScene());
        StartCoroutine(FadeOut(audioSource, timeBeforeLoadScene-1));
    }

    public IEnumerator ActionBeforeNextScene()
    {
        clickSound.Play();
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(timeBeforeLoadScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
            yield return null;
        }

        audioSource.volume = 0f;
    }

    public void ExitGame()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void OnOffSettings()
    {
        clickSound.Play();
        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
        }
        else { settingsPanel.SetActive(true); }
    }

    public void OnOffLoading()
    {
        clickSound.Play();
        if (loadPanel.activeInHierarchy)
        {
            loadPanel.SetActive(false);
        }
        else { loadPanel.SetActive(true); }
    }
}
