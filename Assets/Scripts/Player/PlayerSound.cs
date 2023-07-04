// 작성자 : 박재만
// 작성일 : 2023-07-04

using UnityEngine;

/// <summary>
/// 플레이어의 소리를 담당하는 클래스
/// </summary>
public class PlayerSound : MonoBehaviour
{
    #region Variable

    [Header("BackgroundSound")]
    [SerializeField] private AudioClip _trainBackgroundSound;

    private AudioClip _attackSound; // 공격 효과음
    private AudioClip _skillSound;  // 스킬 효과음

    private Player _player; // 플레이어 데이터 담당 클래스

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        InitData(); // 초기화 진행
        PlayTrainBackgroundSound();
    }

    #endregion

    private void Init()
    {
        _player = GetComponent<Player>();
    }

    /// <summary>
    /// 초기화를 담당하는 함수
    /// </summary>
    private void InitData()
    {
        _attackSound = _player.AttackSound;
        _skillSound = _player.SkillSound;
    }

    public void PlayTrainBackgroundSound()
    {
        SoundManager.Instance.BgSoundPlay(_trainBackgroundSound);
    }

    /// <summary>
    /// 공격 효과음을 실행하는 함수
    /// </summary>
    public void PlayAttackSound()
    {
        SoundManager.Instance.SFXPlay(_attackSound.name, _attackSound); // 공격 효과음 실행
    }

    /// <summary>
    /// 스킬 효과음을 실행하는 함수
    /// </summary>
    public void PlaySkillSound()
    {
        SoundManager.Instance.SFXPlay(_skillSound.name, _skillSound); // 스킬 효과음 실행
    }

    #endregion
}
