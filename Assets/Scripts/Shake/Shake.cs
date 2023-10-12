using UnityEngine;
using Cinemachine;
using System.Collections;

/// <summary>
/// 흔들림을 담당하는 클래스
/// </summary>
public class Shake : MonoBehaviour
{
    #region Variable

    [Header("Cinemachine")]
    [CinemachineImpulseDefinitionProperty]
    public CinemachineImpulseDefinition _impulseDefinition = new CinemachineImpulseDefinition(); // 시네머신 임펄스 변수

    [Header("Noise")]
    [SerializeField] private NoiseSettings _baseNoise;
    [SerializeField] private NoiseSettings _crashNoise;

    private IEnumerator _shakeCor;

    private float _lastEventTime = 0;

    private float _crashAmplitude = 10f;
    private float _crashFrequency = 10f;
    private float _crashDuration = 0.5f;

    #endregion

    #region Function

    /// <summary>
    /// 흔들림 시작 함수
    /// <br/> 중복 검사 후 실행
    /// </summary>
    public void StartShake()
    {
        if (_shakeCor == null) // 중복 검사
        {
            StartCoroutine(ShakeCor()); // 실행
        }
    }

    /// <summary>
    /// 흔들림 정지 함수
    /// </summary>
    public void StopShake()
    {
        StopAllCoroutines();
        _shakeCor = null;
    }

    /// <summary>
    /// 흔들림 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShakeCor()
    {
        Camera camera = Camera.main;
        _shakeCor = ShakeCor(); // 코루틴 할당
        _impulseDefinition.m_RawSignal = _baseNoise;
        while (true)
        {
            // 전 흔들림 이벤트 시간의 종료 시간에 맞춰 실행
            var now = Time.time;
            float eventLength = _impulseDefinition.m_TimeEnvelope.m_AttackTime + _impulseDefinition.m_TimeEnvelope.m_SustainTime; // 이벤트 시간
            if (now - _lastEventTime > eventLength) // 전 흔들림이 끝났다면
            {
                _impulseDefinition.CreateEvent(camera.transform.position, Vector3.down); // 흔들림 이벤트 생성
                _lastEventTime = now;                                             // 이벤트 시간 할당
            }
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Crash 흔들림 변경 함수
    /// </summary>
    public void ChangeCrashShake()
    {
        StartCoroutine(ChangeCrashShakeCor());
    }

    /// <summary>
    /// 지속시간 동안 Crash 흔들림을 유지하는 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeCrashShakeCor()
    {
        _impulseDefinition.m_RawSignal = _crashNoise; // 현재 흔들림 방식을 크래쉬 흔들림 방식으로 변경

        // 현재 흔들림 변수 저장
        float originAmplitude = _impulseDefinition.m_AmplitudeGain;
        float originFrequency = _impulseDefinition.m_FrequencyGain;

        // 흔들림 값 변경
        _impulseDefinition.m_AmplitudeGain = _crashAmplitude;
        _impulseDefinition.m_FrequencyGain = _crashFrequency;

        yield return new WaitForSeconds(_crashDuration); // 지속시간 동안 대기

        _impulseDefinition.m_RawSignal = _baseNoise;     // 기존 흔들림 방식으로 되돌리기

        // 기존 흔들림 변수 값으로 되돌리기
        _impulseDefinition.m_AmplitudeGain = originAmplitude;
        _impulseDefinition.m_FrequencyGain = originFrequency;

        IngameUIController.Instance.UpdateShakeUI(ShakeManager.Instance.ShakeAmount); // UI 초기화
    }

    #endregion
}
