using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] PlayerRessources playerRessources;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = playerRessources.healthMaxAmount;
        healthSlider.value = playerRessources.healthCurrentAmount;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerRessources.healthCurrentAmount;
    }
}
