using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    //General Variables
    public GameObject achievementPanel;
    //public AudioSource achievementSound;
    public bool achievementIsActive = false;
    public GameObject achievementTitle;
    public GameObject achievementDescription;

    //Specific
    //public static int achievement01Count;
    //public int achievementTrigger = 5;
    public GameObject achievementImage01;
    public int achievementCode01;
    public bool hasReachedCP01;


    // Update is called once per frame
    void Update()
    {
        achievementCode01 = PlayerPrefs.GetInt("Achievement01");

        if (hasReachedCP01 && !achievementIsActive && achievementCode01 != 42345)
        {
            Debug.Log("start coroutine achievement");

            StartCoroutine(Trigger01Ach());

        }
    }

    public IEnumerator Trigger01Ach()
    {
        achievementIsActive  = true;
        achievementCode01 = 42345;
        PlayerPrefs.SetInt("Achievement01", achievementCode01);
        
        //achievementSound.Play();
        achievementImage01.SetActive(true);
        achievementTitle.GetComponent<TextMeshProUGUI>().text = "You made it!";
        achievementDescription.GetComponent<TextMeshProUGUI>().text = "Made it to the first checkpoint";
        achievementPanel.SetActive(true);

        yield return new WaitForSeconds(3);


        //Resetting UI
        achievementPanel.SetActive(false);
        achievementImage01.SetActive(false);
        achievementTitle.GetComponent<TextMeshProUGUI>().text = "";
        achievementTitle.GetComponent<TextMeshProUGUI>().text = "";
        achievementIsActive = false;
    }

}
