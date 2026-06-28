using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONOFFSettings : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] AudioSource clickSound;


    public void OnOffSettings()
    {
        if (clickSound != null) { clickSound.Play(); }

        
        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
        }
        else { settingsPanel.SetActive(true); }
    }
}
