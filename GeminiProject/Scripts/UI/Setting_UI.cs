using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    [SerializeField] private UtilitySO utils;
    [SerializeField] private SO_SoundManager soundManager;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void OnEnable()
    {
        utils = Resources.Load<UtilitySO>("UtilitySO");
        soundManager = Resources.Load<SO_SoundManager>("SO_SoundManager");
        masterSlider.value = utils.masterVolume;
        musicSlider.value = utils.musicVolume;
        SFXSlider.value = utils.sfxVolume;
    }

    public void MasterValueChange()
    {
        utils.masterVolume = masterSlider.value;
        if (utils.masterVolume < utils.musicVolume)
        {
            utils.musicVolume = utils.masterVolume;
            musicSlider.value = utils.masterVolume;
        }
        if (utils.masterVolume < utils.sfxVolume)
        {
            utils.sfxVolume = utils.masterVolume;
            SFXSlider.value = utils.sfxVolume;
        }
    }

    public void MusicValueChange()
    {
        utils.musicVolume = musicSlider.value;
        if (utils.musicVolume > utils.masterVolume)
        {
            utils.masterVolume = utils.musicVolume;
            masterSlider.value = utils.musicVolume;
        }
        soundManager.ChangeMusicvolume();
    }

    public void SFXValueChange()
    {
        utils.sfxVolume = SFXSlider.value;
        if (utils.sfxVolume > utils.masterVolume)
        {
            utils.masterVolume = utils.sfxVolume;
            masterSlider.value = utils.sfxVolume;
        }
    }
}
