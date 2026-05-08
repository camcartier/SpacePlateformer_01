using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelSlider : MonoBehaviour
{
    [SerializeField] Slider fuelSlider;
    [SerializeField] PlayerRessources playerRessources;

    // Start is called before the first frame update
    void Start()
    {
        fuelSlider.maxValue = playerRessources.fuelMaxAmount;
        fuelSlider.value = playerRessources.fuelCurrentAmount;
    }

    // Update is called once per frame
    void Update()
    {
        fuelSlider.value = playerRessources.fuelCurrentAmount;
    }
}
