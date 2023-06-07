using UnityEngine;
using Cinemachine;
using System.Collections;

/// <summary>
/// 흔들림 증가를 담당하는 클래스
/// </summary>
public class IncreaseShake : MonoBehaviour
{
    #region Variable

    [Header("UI")]
    [SerializeField] private ShakeSlider _shakeSlider;

    IEnumerator _IncreaseShakeCor;

    CinemachineImpulseDefinition _impulseDefinition;  // 시네머신 임펄스 변수

    public CinemachineImpulseDefinition ImpulseDefinition { get { return _impulseDefinition; } }
    #endregion

    #region Function

    #region LifeCycle

    private void Start()
    {
        Init(); // Awake에서 진행시, 아직 초기화가 되지 않아, Start에서 실행
    }

    #endregion

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _impulseDefinition = GetComponent<Shake>()._impulseDefinition; // Shake에 존재하는 시네머신 임펄스 강도
    }

    /// <summary>
    /// 흔들림 증가 시작 함수
    /// <br/> 중복 검사 후 실행
    /// </summary>
    /// <param name="waitTime">대기 시간</param>
    /// <param name="maxValue">최대값</param>
    public void StartIncreaseShake(float waitTime, float maxValue)
    {
        if (_IncreaseShakeCor == null) // 중복 검사
        {
            StartCoroutine(IncreaseShakeCor(waitTime, maxValue));
        }
    }

    /// <summary>
    /// 흔들림 정지 함수
    /// </summary>
    public void StopIncreaseShake()
    {
        StopCoroutine(_IncreaseShakeCor);
    }

    public void ClearShake()
    {
        _impulseDefinition.m_AmplitudeGain = 0.0f;
    }

    public void DecreaseShake(float value)
    {
        _impulseDefinition.m_AmplitudeGain -= value;
    }

    /// <summary>
    /// 흔들림 증가 코루틴 함수
    /// </summary>
    /// <param name="waitTime">대기 시간</param>
    /// <param name="maxValue">최대값</param>
    /// <returns></returns>
    private IEnumerator IncreaseShakeCor(float waitTime, float maxValue)
    {
        _IncreaseShakeCor = IncreaseShakeCor(waitTime, maxValue); // 코루틴 할당
        while (true)
        {
            yield return new WaitForSeconds(waitTime); // 대기 시간 만큼 대기
            if (_impulseDefinition.m_AmplitudeGain < maxValue) // 최대값이 아니라면
            {
                _impulseDefinition.m_AmplitudeGain++; // 1 증가
                _shakeSlider.ChangeUI(_impulseDefinition.m_AmplitudeGain);
            }
        }
    }

    #endregion
}
