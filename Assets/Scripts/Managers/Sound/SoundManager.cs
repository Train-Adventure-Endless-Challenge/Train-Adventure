using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : GlobalSingleton<SoundManager>
{
    private AudioSource _bgSoundSource;                         // bgm을 실행시킬 source

    [Header("AudioMixer")]
    [SerializeField] private AudioMixerGroup sfxMixerGroup;     // sfx의 mixerGroup을 받아옴

    [Header("Sound")]
    [SerializeField] private AudioClip _trainBackgroundSound;
    [SerializeField] private AudioClip _titleBackgroundSound;
    [SerializeField] private AudioClip _onClickSound;

    [Header("Variable")]
    [SerializeField] private float _maxBackgroundSoundVolume = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Init()
    {
        _bgSoundSource = GetComponent<AudioSource>();
        TitleBgSoundPlay(); // 타이틀 배경음악 실행
    }
    /// <summary>
    /// 효과음을 재생 시키는 함수
    /// </summary>
    /// <param name="sfxName">효과음 이름</param>
    /// <param name="clip">효과음 clip</param>
    public void SFXPlay(AudioClip clip)
    {
        GameObject go = new GameObject(clip.name);
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
        _bgSoundSource.volume = _maxBackgroundSoundVolume;
        _bgSoundSource.Play();
    }
    
    /// <summary>
    /// 타이틀 배경음악을 재생하는 함수
    /// </summary>
    public void TitleBgSoundPlay() => BgSoundPlay(_titleBackgroundSound); // 타이틀 배경음악 재생

    /// <summary>
    /// 기차의 배경음악을 재생하는 함수
    /// </summary>
    public void TrainBgSoundPlay() => BgSoundPlay(_trainBackgroundSound); // 기차 배경음악 재생

    /// <summary>
    /// 버튼 클릭 효과음을 재생하는 함수
    /// </summary>
    public void PlayButtonClickSound() => SFXPlay(_onClickSound); // 클릭 효과음 재생
}
