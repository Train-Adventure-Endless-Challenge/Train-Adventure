using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : GlobalSingleton<SoundManager>
{
    private AudioSource _bgSoundSource;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    private void Start()
    {
        _bgSoundSource= GetComponent<AudioSource>();
    }
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = sfxMixerGroup;
        audiosource.clip = clip;
        audiosource.Play();
        

        Destroy(go, clip.length);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        _bgSoundSource.clip = clip;
        _bgSoundSource.loop = true;
        _bgSoundSource.volume = 0.1f; 
        _bgSoundSource.Play();
    }
}
