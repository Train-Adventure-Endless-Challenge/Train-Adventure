using UnityEngine;

/// <summary>
/// 흔들림을 관리하는 클래스
/// </summary>
public class ShakeManager : SceneSingleton<ShakeManager>
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private float _waitTime = 3.0f;  // 대기 시간
    [SerializeField] private float _maxValue = 10.0f; // 흔들림 최대값

    private Shake _shake;

    public float ShakeAmount { get { return _shake._impulseDefinition.m_AmplitudeGain * 2; } }

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        _shake = GetComponent<Shake>();
    }

    private void Start()
    {
        _shake.StartShake();
    }

    #endregion

    public void ClearShake()
    {
        _shake._impulseDefinition.m_AmplitudeGain = 0;
        IngameUIController.Instance.UpdateShakeUI(ShakeAmount); // UI 업데이트
        UpdateShake();
    }

    public void DecreaseShake(float value)
    {
        _shake._impulseDefinition.m_AmplitudeGain -= value;
        UpdateShake();
    }

    public void IncreaseShake(float value)
    {
        _shake._impulseDefinition.m_AmplitudeGain += value / 2; // 증가

        if (ShakeAmount > _maxValue)
        {
            _shake._impulseDefinition.m_AmplitudeGain = _maxValue / 2;
        }

        IngameUIController.Instance.UpdateShakeUI(ShakeAmount); // UI 업데이트
        UpdateShake();
    }

    private void UpdateShake()
    {
        PlayerManager.Instance.UseShakeDebuff((int)ShakeAmount);
    }

    /// <summary>
    /// Crash 흔들림 변경 함수
    /// </summary>
    public void ChangeCrashShake()
    {
        _shake.ChangeCrashShake();
    }

    #endregion
}