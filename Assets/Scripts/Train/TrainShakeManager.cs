using UnityEngine;
using Cinemachine;
using System.Collections;

public class TrainShakeManager : SceneSingleton<TrainManager>
{
    [CinemachineImpulseDefinitionProperty]
    public CinemachineImpulseDefinition ImpulseDefinition = new CinemachineImpulseDefinition();

    [SerializeField] private float _waitTime = 3.0f;
    [SerializeField] private float _maxValue = 10.0f;

    IEnumerator _shakeCor;
    IEnumerator _IncreaseShakeCor;

    float LastEventTime = 0;

    private void Start()
    {
        StartShake();
        StartIncreaseShake();
    }

    public void StartShake()
    {
        if (_shakeCor == null)
        {
            StartCoroutine(ShakeCor());
        }
    }

    public void StartIncreaseShake()
    {
        if (_IncreaseShakeCor == null)
        {
            StartCoroutine(IncreaseShakeCor());
        }
    }

    public void StartShake(float value)
    {
        ImpulseDefinition.m_AmplitudeGain = value;
        if (_shakeCor == null)
            StartCoroutine(ShakeCor());
    }

    public void ChangeAmplitude(float value)
    {
        ImpulseDefinition.m_AmplitudeGain = value;
    }

    public void StopShake()
    {
        StopCoroutine(_shakeCor);
    }

    private IEnumerator IncreaseShakeCor()
    {
        _IncreaseShakeCor = IncreaseShakeCor();
        while (true)
        {
            yield return new WaitForSeconds(_waitTime);
            if (ImpulseDefinition.m_AmplitudeGain < _maxValue)
            {
                ImpulseDefinition.m_AmplitudeGain++;
            }
        }
    }

    private IEnumerator ShakeCor()
    {
        _shakeCor = ShakeCor();
        while (true)
        {
            var now = Time.time;
            float eventLength = ImpulseDefinition.m_TimeEnvelope.m_AttackTime + ImpulseDefinition.m_TimeEnvelope.m_SustainTime;
            if (now - LastEventTime > eventLength)
            {
                ImpulseDefinition.CreateEvent(transform.position, Vector3.down);
                LastEventTime = now;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}