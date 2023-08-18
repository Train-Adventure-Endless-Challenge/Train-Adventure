using Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    #region Variable

    #region FOV

    [Header("FOV")]
    [SerializeField] private float _originFOV;              // 기본 FOV
    [SerializeField] private float _attackZooomFOV = 47.5f; // 공격 줌 FOV
    [SerializeField] private float _skillZoomFOV = 45f;     // 스킬 줌 FOV

    #endregion

    #region Time

    [Header("Time")]
    [SerializeField] private float _lerpTime = 0.1f; // 최종 러프 시간

    #endregion

    #region Curve

    [Header("Curve")]
    [SerializeField] private AnimationCurve _animationCurve; // 시간에 따른 변화 커브값

    #endregion

    private CinemachineVirtualCamera _cinemachineVirtualCamera; // 시네머신 카메라

    private Coroutine _joomInCor;  // 줌 인 코루틴 변수
    private Coroutine _joomOutCor; // 줌 아웃 코루틴 변수

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init(); // 초기화 진행
    }

    #endregion

    #region Joom

    /// <summary>
    /// 초기화를 담당하는 함수
    /// <br/>
    /// 카메라 및 변수 초기화
    /// </summary>
    private void Init()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>(); // 카메라 초기화

        _originFOV = _cinemachineVirtualCamera.m_Lens.FieldOfView; // 최댓값 초기화
    }

    /// <summary>
    /// 줌을 실행하는 함수
    /// </summary>
    public void Joom(bool isSkill)
    {
        if (_joomInCor != null) // 줌 실행 중이라면
        {
            return;
        }
        if (isSkill == true)
        {
            _joomInCor = StartCoroutine(JoomInCor(_skillZoomFOV)); // 스킬 줌인 실행
        }
        else if (isSkill == false)
        {
            _joomInCor = StartCoroutine(JoomInCor(_attackZooomFOV)); // 공격 줌인 실행
        }
    }

    /// <summary>
    /// 줌 인을 담당하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator JoomInCor(float zoomValue)
    {
        float currentTime = 0f;
        while (currentTime < _lerpTime)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= _lerpTime)
            {
                currentTime = _lerpTime;
            }
            float curveValue = _animationCurve.Evaluate(currentTime / _lerpTime);

            _cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_originFOV, zoomValue, curveValue); // 시간에 따른 줌 인 실행

            yield return new WaitForEndOfFrame();
        }
        _joomOutCor = StartCoroutine(JoomOutCor()); // 줌 아웃 실행
        _joomInCor = null;
    }

    /// <summary>
    /// 줌 아웃을 담당하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator JoomOutCor()
    {
        float currentTime = 0f;
        float currentFOV = _cinemachineVirtualCamera.m_Lens.FieldOfView;
        while (currentTime < _lerpTime)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= _lerpTime)
            {
                currentTime = _lerpTime;
            }
            float curveValue = _animationCurve.Evaluate(currentTime / _lerpTime);

            _cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(currentFOV, _originFOV, curveValue); // 시간에 따른 줌 아웃 실행

            yield return new WaitForEndOfFrame();
        }
        _joomOutCor = null;
    }

    #endregion

    #endregion
}