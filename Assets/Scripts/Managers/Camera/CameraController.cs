using Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _maxFOV;
    [SerializeField] private float _minFOV = 40f;

    [SerializeField] private float _lerpTime = 0.4f;
    [SerializeField] private float _waitOffset = 0.4f;

    [SerializeField] private AnimationCurve _animationCurve;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private Coroutine _joomInCor;
    private Coroutine _joomOutCor;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _maxFOV = _cinemachineVirtualCamera.m_Lens.FieldOfView;
    }

    public void Joom()
    {
        if (_joomInCor == null)
        {
            _joomInCor = StartCoroutine(JoomInCor());
        }
    }

    private IEnumerator JoomInCor()
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

            _cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_maxFOV, _minFOV, curveValue);

            yield return new WaitForEndOfFrame();
        }
        _joomOutCor = StartCoroutine(JoomOutCor());
        _joomInCor = null;
    }

    private IEnumerator JoomOutCor()
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

            _cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_minFOV, _maxFOV, curveValue);

            yield return new WaitForEndOfFrame();
        }
        _joomOutCor = null;
    }
}
