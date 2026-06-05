using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayLetters : MonoBehaviour
{
    public float typingSpeed;
    [SerializeField] TextMeshProUGUI TXT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator DisplayTitle(string title)
    {

        TXT.text = "";

        foreach (char letter in title.ToCharArray())
        {
            TXT.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

}
