using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerRessources : ScriptableObject
{
    
    public float fuelCurrentAmount;
    public float fuelMaxAmount;
    
    public float refuelTimerDuration;

    public float currentRefuelSpeed;
    public float refuelSpeed;
    public float refuelAcceleration;

    public float playerCurrentLife;
    public float playerMaxLife;
}
