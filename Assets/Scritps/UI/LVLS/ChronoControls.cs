using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChronoControls : MonoBehaviour
{
    private float timer;
    [SerializeField] TextMeshProUGUI chronoTXT;

    //public bool canUpdate;
    //[SerializeField] GameManager gameManager;
    [SerializeField] PlayerStateMachine playerStateMachine;

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
}
