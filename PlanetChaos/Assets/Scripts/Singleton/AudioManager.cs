using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void OnMasterVolumeChange(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void OnBGMVolumeChange(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
    }

    public void OnSFXVolumeChange(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
}
