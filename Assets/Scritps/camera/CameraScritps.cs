using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScritps : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2dPlayer;

    public CinemachineVirtualCamera VirtualCamera;

    // Start is called before the first frame update
    void Start()
    {

        //VirtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(rb2dPlayer.velocity.y <=0)
        //{
          //  VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0f;
        //}
        //else { VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1f; }
    }
}
