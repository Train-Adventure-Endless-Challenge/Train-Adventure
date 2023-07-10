using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 씬의 충돌 볼륨을 관리하는 클래스
/// </summary>
public class HitVolume : MonoBehaviour
{
    #region Variable

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

    public void ChangeVolume(float hp)
    {
        _vignette.smoothness.value = hp; // 플레이어 체력량에 따라 굵어짐
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