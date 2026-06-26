using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public void InstantiateBullet(GameObject bullet, Vector3 initPosition, Quaternion quaternion)
    {
        Instantiate(bullet, initPosition, quaternion);
    }

    public ParticleSystem InstantiatePS(ParticleSystem toInstantiate, Vector3 initPosition, Quaternion quaternion)
    {
        return Instantiate(toInstantiate, initPosition, quaternion);
    }
}
