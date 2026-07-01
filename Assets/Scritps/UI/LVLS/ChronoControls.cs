using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChronoControls : MonoBehaviour
{
    public float timer;
    [SerializeField] TextMeshProUGUI chronoTXT;

    //public bool canUpdate;
    //[SerializeField] GameManager gameManager;


    [SerializeField] PlayerStateMachine playerStateMachine;

    //public List<float> highscores = new List<float>(); 

    // Update is called once per frame
    void Update()
    {
        if (!playerStateMachine.isDialog)
        {

            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);

            chronoTXT.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }

    public void UpdateChrono()
    {

    }



    /*
    void SaveScores()
    {
        for (int i = 0; i < highscores.Count; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, highscores[i]);
        }

        PlayerPrefs.SetInt("ScoreCount", highscores.Count);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        highscores.Clear();

        int count = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < count; i++)
        {
            highscores.Add(PlayerPrefs.GetFloat("HighScore" + i));
        }
    }*/
}


