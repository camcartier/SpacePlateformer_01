using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerRessources : ScriptableObject
{
    [Header("Fuel")]
    public float fuelCurrentAmount;
    public float fuelMaxAmount;

    [Header("Refuel")]
    public float refuelTimerDuration;
    public float currentRefuelSpeed;
    public float refuelSpeed;
    public float refuelAcceleration;

    [Header("Health")]
    public float healthCurrentAmount;
    public float healthMaxAmount;
}
