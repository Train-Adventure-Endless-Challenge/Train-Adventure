using Cinemachine;
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
    private IncreaseShake _increaseShake;

    public float ShakeAmount { get { return _increaseShake.ImpulseDefinition.m_AmplitudeGain; } }
    
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

    public void ClearShake()
    {
        _increaseShake.ClearShake();
    }

    public void DecreaseShake(float value)
    {
        _shake._impulseDefinition.m_AmplitudeGain -= value;
    }

    public void IncreaseShake()
    {
        _shake._impulseDefinition.m_AmplitudeGain += 1; // 증가
    }
    #endregion

    #endregion
}