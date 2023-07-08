using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;

    public void setMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void setMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void setSFXVolume(float sliderValue)
    {
        sfxMixer.SetFloat("sfxVolume", Mathf.Log10(sliderValue) * 20);
    }
}
