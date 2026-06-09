using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashResetTImer : MonoBehaviour
{
    [SerializeField] PlayerStateMachine playerStateMachine;
    [SerializeField] PlayerData playerData;

    void Start()
    {
        playerStateMachine.dashResetTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStateMachine.dashResetTimer > playerData.dashReset)
        {
            playerStateMachine.dashResetTimer = playerData.dashReset;
        }
        else
        {
            playerStateMachine.dashResetTimer += Time.deltaTime;
        }
           



    }
}
