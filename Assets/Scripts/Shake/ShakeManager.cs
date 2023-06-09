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

    public float ShakeAmount { get { return _increaseShake.ImpulseDefinition.m_AmplitudeGain; } set { _increaseShake.ImpulseDefinition.m_AmplitudeGain = value; } }
    
    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        _shake = GetComponent<Shake>();
        _increaseShake = GetComponent<IncreaseShake>();
    }

    private void Start()
    {
        _shake.StartShake();
        _increaseShake.StartIncreaseShake(_waitTime, _maxValue);
    }

    public void ClearShake()
    {
        _increaseShake.ClearShake();
    }
    #endregion

    #endregion
}