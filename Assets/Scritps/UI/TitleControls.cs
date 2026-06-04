using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleControls : MonoBehaviour
{
    public TextMeshProUGUI titleTXT;
    public float typingSpeed = 0.04f;

    
    void Start()
    {
        //GetComponent<Animator>().enabled = false;
        string title = titleTXT.text;
        //StartCoroutine(DisplayTitle(title));
        //StartCoroutine(DisplayTitle2(title));
    }

    
    void Update()
    {
        
    }

    public IEnumerator DisplayTitle(string title)
    {
        titleTXT.text = "";

        foreach (char letter in title.ToCharArray())
        {
            titleTXT.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator DisplayTitle2(string title)
    {
        titleTXT.text = "";

        foreach (char letter in title)
        {
            Debug.Log("Ajout : " + letter);

            titleTXT.text += letter;

            Debug.Log("Texte actuel : " + titleTXT.text);

            yield return new WaitForSeconds(typingSpeed);
        }

        Debug.Log("Coroutine terminée");
    }
}
