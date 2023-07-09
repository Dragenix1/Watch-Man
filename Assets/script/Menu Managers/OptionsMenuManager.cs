using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private LoadAndSaveOptions loadAndSave;
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;

    public AudioMixer MasterMixer { get => masterMixer; set => masterMixer = value; }
    public AudioMixer MusicMixer { get => musicMixer; set => musicMixer = value; }
    public AudioMixer SfxMixer { get => sfxMixer; set => sfxMixer = value; }

    public void setMasterVolume(float sliderValue)
    {
        masterMixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
        loadAndSave.UpdateMaster(sliderValue);
    }

    public void setMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
        loadAndSave.UpdateMusic(sliderValue);
    }

    public void setSFXVolume(float sliderValue)
    {
        sfxMixer.SetFloat("sfxVolume", Mathf.Log10(sliderValue) * 20);
        loadAndSave.UpdateSFX(sliderValue);
    }
}
