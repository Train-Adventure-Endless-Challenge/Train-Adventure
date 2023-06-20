using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GlobalSingleton<SoundManager>
{
    private AudioSource _bgSoundSource;
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
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
