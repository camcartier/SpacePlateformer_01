using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using TMPro.Examples;

public class TXTManagerStart : MonoBehaviour
{
    public string[] arrayOfText;
    [SerializeField] TextMeshProUGUI displayTXT;
    private int currentTXTIndex;
    
    [SerializeField] GameObject startImage;
    public GameObject[] arrayOfBGImages = new GameObject[6];


    public string[] arrayOfNames = new string[6];
    [SerializeField] TextMeshProUGUI namesTXT;
    //private int currentNamesTXTIndex;

    [SerializeField] GameObject narrationPanel;
    [SerializeField] GameObject iconeImage;
    [SerializeField] GameObject UIPanel;

    [SerializeField] PlayerStateMachine playerStateMachine;

    public bool narrationIsOver;
    public bool dialogIsAtStart;

    public float typingSpeed;

    private Coroutine displayTXTCoroutine;

    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource talkingSound1;

    private InfoHolder infoHolder;

    private void Awake()
    {
        infoHolder = InfoHolder.Instance;
    }

    void Start()
    {


        if (infoHolder != null) {

            if (!infoHolder.dialogHasHappened)
            {
                dialogIsAtStart = true;
                narrationPanel.SetActive(true);
                iconeImage.SetActive(true);

                if (UIPanel != null) { UIPanel.SetActive(false); }
                

                displayTXT.text = string.Empty;

                StartDialog();
            }
            else
            {
                narrationPanel.SetActive(false);
                iconeImage.SetActive(false);
            }
        }



        /*
        if (infoHolder.dialogHasHappened) { Debug.Log("je vois que le dialoque est passé"); }
        else { Debug.Log("le dialoque n'est pas passé"); }

        Debug.Log("Référence sérialisée : " + infoHolder.GetInstanceID());
        Debug.Log("Singleton : " + InfoHolder.Instance.GetInstanceID());

        Debug.Log("Valeur sérialisée : " + infoHolder.dialogHasHappened);
        Debug.Log("Valeur singleton : " + InfoHolder.Instance.dialogHasHappened);
        */
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



    private void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void StartDialog()
    {
        currentTXTIndex = 0;

        talkingSound1.Play();

        displayTXTCoroutine = StartCoroutine(DisplayLetters());
    }

    public IEnumerator DisplayLetters()
    {

        

        foreach (char letter in arrayOfText[currentTXTIndex].ToCharArray())
        {
            displayTXT.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        talkingSound1.Stop();
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

            talkingSound1.Play();

            displayTXTCoroutine = StartCoroutine(DisplayLetters());
        }
        else
        {
            narrationPanel.SetActive(false);
            iconeImage.SetActive(false);
            playerStateMachine.isDialog = false;
            narrationIsOver = true;

            if (UIPanel != null) { UIPanel.SetActive(true); }

            
        }

    }

}
