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
    [SerializeField] private AudioClip _trainBackgroundSound; // 일반 배경음악

    private AudioClip _attackSound;       // 일반 공격 효과음
    private AudioClip _weaponAttackSound; // 무기 공격 효과음
    private AudioClip _skillSound;        // 스킬 효과음

    private Player _player; // 플레이어 데이터 담당 클래스

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화 진행
    }

    private void Start()
    {
        InitData();                 // 데이터 초기화 진행
        PlayTrainBackgroundSound(); // 배경음악 실행
    }

    #endregion

    /// <summary>
    /// 초기화를 담당하는 함수
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
    }

    /// <summary>
    /// 데이터 초기화를 담당하는 함수
    /// </summary>
    private void InitData()
    {
        _attackSound = _player.AttackSound;
        _weaponAttackSound = _player.WeaponAttackSound;
        _skillSound = _player.SkillSound;
    }

    /// <summary>
    /// 배경음악을 실행하는 함수
    /// </summary>
    public void PlayTrainBackgroundSound()
    {
        SoundManager.Instance.BgSoundPlay(_trainBackgroundSound); // 배경음악 실행
    }

    /// <summary>
    /// 일반 공격 효과음을 실행하는 함수
    /// </summary>
    public void PlayAttackSound()
    {
        SoundManager.Instance.SFXPlay(_attackSound.name, _attackSound); // 공격 효과음 실행
    }

    public void PlayWeaponAttackSound()
    {
        SoundManager.Instance.SFXPlay(_weaponAttackSound.name, _weaponAttackSound);
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
