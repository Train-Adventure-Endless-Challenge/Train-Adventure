using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    #region Variable

    [SerializeField] private AudioMixer mixer;    // AudioMixer

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;
    #endregion

    #region Function
    public void BGSoundVolume(float val)
    {
        mixer.SetFloat("BGM", val);
    }

    public void SFXVolume(float val)
    {
        mixer.SetFloat("SFX", val);
    }

    public void InitSoundVolume()
    {
        sfxSlider.value = 0;
        bgmSlider.value = 0;
    }

    #endregion

}
