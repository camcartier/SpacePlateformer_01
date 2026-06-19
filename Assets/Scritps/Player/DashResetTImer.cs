using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashResetTImer : MonoBehaviour
{
    [SerializeField] PlayerStateMachine playerStateMachine;
    [SerializeField] PlayerData playerData;

    [SerializeField] RawImage dashIcon;

    void Start()
    {
        playerStateMachine.dashResetTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStateMachine.dashResetTimer >= playerData.dashReset)
        {
            playerStateMachine.dashResetTimer = playerData.dashReset;
            dashIcon.color = Color.green;
        }
        else
        {
            playerStateMachine.dashResetTimer += Time.deltaTime;
            dashIcon.color = Color.red;
        }
           



    }
}
