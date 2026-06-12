using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControls : MonoBehaviour
{
    private CheckPointNumber chekPointNumber;
    private AchievementManager achievementManager;

    // Start is called before the first frame update
    void Start()
    {
        achievementManager = GameObject.Find("AchievementManager").GetComponent<AchievementManager>();
        chekPointNumber = GetComponent<CheckPointNumber>();

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("player seen");

            if (chekPointNumber.number == 1)
            {
                //Debug.Log("number is correct");
                achievementManager.hasReachedCP01 = true;
            }
        }
    }
}
