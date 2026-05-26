using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParrallax : MonoBehaviour
{
    private float startPosY;
    private float startPosX;
    public float XparallaxStrength;
    public float YparallaxStrength;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float distanceX = mainCamera.transform.position.x * XparallaxStrength;
        float distanceY = mainCamera.transform.position.y * YparallaxStrength;

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
    }
}
