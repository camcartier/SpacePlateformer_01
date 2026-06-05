using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introDialogControls : MonoBehaviour
{
    public GameObject canvas;

    private void Start()
    {
        StartCoroutine(WaitBeforeActivate());
    }

    public IEnumerator WaitBeforeActivate()
    {
        yield return new WaitForSeconds(1f);
        canvas.SetActive(true);
    }
}
