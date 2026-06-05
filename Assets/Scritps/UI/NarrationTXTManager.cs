using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NarrationTXTManager : MonoBehaviour
{
    public string[] arrayOfText = new string[6];
    [SerializeField] TextMeshProUGUI displayTXT;
    private int currentTXTIndex;
    
    [SerializeField] GameObject startImage;
    public GameObject[] arrayOfBGImages = new GameObject[6];


    public string[] arrayOfNames = new string[6];
    [SerializeField] TextMeshProUGUI namesTXT;
    //private int currentNamesTXTIndex;

    [SerializeField] GameObject narrationPanel;
    [SerializeField] GameObject iconeImage;

    [SerializeField] PlayerStateMachine playerStateMachine;

    public bool narrationIsOver;

    public float typingSpeed;

    [SerializeField] AudioSource clickSound;
   
    void Start()
    {
        currentTXTIndex = 0;

        //tartCoroutine(DisplayLetters(displayTXT.text));

        /*
        if (startImage.activeInHierarchy == false)
        {
            startImage.SetActive(true);
        }*/
    }

   
    void Update()
    {
        displayTXT.text = arrayOfText[currentTXTIndex];
        namesTXT.text = arrayOfNames[currentTXTIndex];

        if (currentTXTIndex > 0)
        {
            if (arrayOfBGImages[currentTXTIndex].activeInHierarchy == false)
            {
                arrayOfBGImages[currentTXTIndex].SetActive(true);
                arrayOfBGImages[currentTXTIndex -1 ].SetActive(false);
            }


        }

    }

    public void IncrementIndex()
    {
        clickSound.Play();
        

        if (currentTXTIndex < arrayOfBGImages.Length - 1)
        {
            currentTXTIndex += 1;
        }
        else
        {
            currentTXTIndex = arrayOfBGImages.Length - 1;
            
            narrationPanel.SetActive(false);
            iconeImage.SetActive(false);
            playerStateMachine.isDialog = false;
            narrationIsOver = true;
            
        }

        
    }


    private void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    public IEnumerator DisplayLetters(string text)
    {

        displayTXT.text = "";

        foreach (char letter in text.ToCharArray())
        {
            displayTXT.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
