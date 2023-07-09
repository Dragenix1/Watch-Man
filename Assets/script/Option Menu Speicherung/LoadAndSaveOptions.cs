using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAndSaveOptions : MonoBehaviour
{
    [SerializeField] private BaseOptionScriptableObject config;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private OptionsMenuManager optionsManager;

    private void Start()
    {
        if(config != null)
        {
            optionsManager.setMasterVolume(config.masterSave);
            optionsManager.setMusicVolume(config.musicSave);
            optionsManager.setSFXVolume(config.sfxSave);

            masterSlider.value = config.masterSave;
            musicSlider.value = config.musicSave;
            sfxSlider.value = config.sfxSave;
        }
    }

    public void UpdateMaster(float value)
    {
        config.masterSave = value;
    }

    public void UpdateMusic(float value)
    {
        config.musicSave = value;
    }

    public void UpdateSFX(float value)
    {
        config.sfxSave = value;
    }
}
