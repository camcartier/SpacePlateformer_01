using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using TMPro.Examples;

public class TXTManager1 : MonoBehaviour
{
    public string[] arrayOfText;
    [SerializeField] TextMeshProUGUI displayTXT;
    private int currentTXTIndex;
    
    [SerializeField] GameObject startImage;
    public GameObject[] arrayOfBGImages = new GameObject[6];


    public string[] arrayOfNames = new string[6];
    [SerializeField] TextMeshProUGUI namesTXT;

    [SerializeField] GameObject narrationPanel;
    [SerializeField] GameObject iconeImage;

    [SerializeField] PlayerStateMachine playerStateMachine;

    public bool narrationIsOver;
    public bool dialogIsAtStart;

    public float typingSpeed;

    private Coroutine displayTXTCoroutine;

    [SerializeField] AudioSource clickSound;

    //private InfoHolder infoHolder;

    private void Awake()
    {
        //infoHolder = InfoHolder.Instance;
    }



    private void OnEnable()
    {

        if (narrationPanel.activeInHierarchy == false)
        {

            narrationPanel.SetActive(true);
            iconeImage.SetActive(true);
            playerStateMachine.isDialog = true;
            Debug.Log("on enable isDialog is"+playerStateMachine.isDialog);  
        }

        displayTXT.text = string.Empty;

        StartDialog();
    }


    void Update()
    {
        //displayTXT.text = arrayOfText[currentTXTIndex];
        
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



    void StartDialog()
    {
        currentTXTIndex = 0;
        displayTXTCoroutine = StartCoroutine(DisplayLetters());
    }

    public IEnumerator DisplayLetters()
    {

        

        foreach (char letter in arrayOfText[currentTXTIndex].ToCharArray())
        {
            displayTXT.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextLine()
    {
        if (displayTXTCoroutine != null)
        {
            StopCoroutine(displayTXTCoroutine);
        }

        if (currentTXTIndex < arrayOfText.Length - 1)
        {
            currentTXTIndex++;
            displayTXT.text = string.Empty;
            playerStateMachine.isDialog = true;

            displayTXTCoroutine = StartCoroutine(DisplayLetters());
        }
        else
        {
            narrationPanel.SetActive(false);
            iconeImage.SetActive(false);
            playerStateMachine.isDialog = false;
            narrationIsOver = true;
        }

    }

}
