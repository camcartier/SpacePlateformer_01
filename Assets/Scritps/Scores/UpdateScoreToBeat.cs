using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScoreToBeat : MonoBehaviour
{
    [SerializeField] SaveScore saveScoresScript;
    [SerializeField] TextMeshProUGUI txtToUpdate;

    // Start is called before the first frame update
    void Start()
    {
        //txtToUpdate.text = saveScoresScript.scores[0].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
