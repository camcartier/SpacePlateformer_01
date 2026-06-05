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


}
