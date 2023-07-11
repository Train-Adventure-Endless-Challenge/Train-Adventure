using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingUI : MonoBehaviour
{
    #region Variable

    [SerializeField] private AudioMixer mixer;    // AudioMixer

    #endregion

    #region Function
    public void BGSoundVolume(float val)
    {
        mixer.SetFloat("BGSoundVolume", Mathf.Log10(val) * 20);
    }

    public void SFXVolume(float val)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20);
    }
    #endregion

}
