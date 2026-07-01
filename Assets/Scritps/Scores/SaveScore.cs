using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SaveScore: MonoBehaviour
{
    [SerializeField] ChronoControls chronoControls;

    public List<float> scores = new List<float>();


    private void Start()
    {
        LoadHighScores();

        foreach (float score in scores)
        {
            Debug.Log(score);
        }
    }



    private void OnApplicationQuit()
    {
        SaveHighScore();

    }

    [ContextMenu("Reset High Scores")]
    public void ResetHighScores()
    {
        PlayerPrefs.DeleteKey("HighScore0");
        PlayerPrefs.DeleteKey("HighScore1");
        PlayerPrefs.DeleteKey("HighScore2");
        PlayerPrefs.DeleteKey("ScoreCount");
        PlayerPrefs.Save();

        Debug.Log("High scores réinitialisés.");
    }

    public void AddHighScore(float score)
    {
        scores.Add(score);

        scores.Sort();

        while (scores.Count > 3)
        {

            scores.RemoveAt(scores.Count - 1);
        }
    }

    public void SaveHighScore()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, scores[i]);
        }

        PlayerPrefs.SetInt("ScoreCount", scores.Count);
        PlayerPrefs.Save();
    }

    public void LoadHighScores()
    {
        scores.Clear();

        //int count = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < 3; i++)
        {
            scores.Add(PlayerPrefs.GetFloat("HighScore" + i, 900f));
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddHighScore(chronoControls.timer);
        }

        //Debug.Log(scores.Count);
        //Debug.Log(scores[0]);
        //Debug.Log(scores[1]);
        //Debug.Log(scores[2]);
        
    }


}

