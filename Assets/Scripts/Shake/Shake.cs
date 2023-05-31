using UnityEngine;
using Cinemachine;
using System.Collections;

/// <summary>
/// 흔들림을 담당하는 클래스
/// </summary>
public class Shake : MonoBehaviour
{
    #region Variable

    [CinemachineImpulseDefinitionProperty]
    public CinemachineImpulseDefinition _impulseDefinition = new CinemachineImpulseDefinition(); // 시네머신 임펄스 변수
    
    private IEnumerator _shakeCor;

    private float _lastEventTime = 0;

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
        StopCoroutine(ShakeCor());
    }

    /// <summary>
    /// 흔들림 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShakeCor()
    {
        _shakeCor = ShakeCor(); // 코루틴 할당
        while (true)
        {
            // 전 흔들림 이벤트 시간의 종료 시간에 맞춰 실행
            var now = Time.time;
            float eventLength = _impulseDefinition.m_TimeEnvelope.m_AttackTime + _impulseDefinition.m_TimeEnvelope.m_SustainTime;
            if (now - _lastEventTime > eventLength) // 전 흔들림이 끝났다면
            {
                _impulseDefinition.CreateEvent(transform.position, Vector3.down); // 흔들림 이벤트 생성
                _lastEventTime = now;                                             // 이벤트 시간 할당
            }
            yield return new WaitForEndOfFrame();
        }
    }

    #endregion
}
