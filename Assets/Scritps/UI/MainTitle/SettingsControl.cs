using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetSfxsVolume(float volume)
    {
        audioMixer.SetFloat("SfxsVolume", volume);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        if (isFullScreen) { Screen.fullScreenMode = FullScreenMode.FullScreenWindow; }
        else { Screen.fullScreenMode = FullScreenMode.Windowed; }
    }
}
