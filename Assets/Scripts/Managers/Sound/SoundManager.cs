using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : GlobalSingleton<SoundManager>
{
    private AudioSource _bgSoundSource;                         // bgm을 실행시킬 source

    [Header("AudioMixer")]
    [SerializeField] private AudioMixerGroup sfxMixerGroup;     // sfx의 mixerGroup을 받아옴

    [Header("SFXClip")]
    [SerializeField] private AudioClip _onClickSound;

    private void Start()
    {
        _bgSoundSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 효과음을 재생 시키는 함수
    /// </summary>
    /// <param name="sfxName">효과음 이름</param>
    /// <param name="clip">효과음 clip</param>
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = sfxMixerGroup;
        audiosource.clip = clip;
        audiosource.Play();


        Destroy(go, clip.length);
    }

    /// <summary>
    /// 배경음악을 재생 시키는 함수
    /// </summary>
    /// <param name="clip">배경음악 clip</param>
    public void BgSoundPlay(AudioClip clip)
    {
        _bgSoundSource.clip = clip;
        _bgSoundSource.loop = true;
        _bgSoundSource.volume = 0.1f;
        _bgSoundSource.Play();
    }

    /// <summary>
    /// 버튼 클릭 효과음을 재생하는 함수
    /// </summary>
    public void PlayButtonClickSound()
    {
        SFXPlay(_onClickSound.name, _onClickSound);
    }
}
