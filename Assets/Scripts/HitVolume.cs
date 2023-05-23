using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 씬의 충돌 볼륨을 관리하는 클래스
/// </summary>
public class HitVolume : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private Player _player;
    [SerializeField] private PlayerData _playerData;

    private Volume _volume;
    private Vignette _vignette;

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init();
    }

    #endregion

    private void OnEnable()
    {
        _vignette.smoothness.value = 1 - _player.Hp / _playerData.Hp; // 플레이어 체력량에 따라 굵어짐
    }

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
    }

    #endregion
}