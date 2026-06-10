using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChronoControls : MonoBehaviour
{
    private float timer;
    [SerializeField] TextMeshProUGUI chronoTXT;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        chronoTXT.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
