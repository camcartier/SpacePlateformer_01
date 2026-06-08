using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using TMPro.Examples;

public class NarrationTXTManager : MonoBehaviour
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

    [SerializeField] PlayerStateMachine playerStateMachine;

    public bool narrationIsOver;
    public bool dialogIsAtStart;

    public float typingSpeed;

    private Coroutine displayTXTCoroutine;

    [SerializeField] AudioSource clickSound;

    private InfoHolder infoHolder;

    private void Awake()
    {
        infoHolder = InfoHolder.Instance;
    }

    void Start()
    {
        //currentTXTIndex = 0;
        if (!infoHolder.dialogHasHappened)
        {
            if (SceneManager.GetActiveScene().name == ("TestLevel1"))
            {
                dialogIsAtStart = true;
            }

            narrationPanel.SetActive(true);
            iconeImage.SetActive(true);

            displayTXT.text = string.Empty;

            StartDialog();
        }
        else
        {
            narrationPanel.SetActive(false);
            iconeImage.SetActive(false);
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

   /* public void IncrementIndex()
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

        
    }*/


    private void LoadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
