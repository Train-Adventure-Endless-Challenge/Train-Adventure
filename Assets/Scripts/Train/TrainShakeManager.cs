using UnityEngine;
using Cinemachine;
using System.Collections;

public class TrainShakeManager : SceneSingleton<TrainManager>
{
    [CinemachineImpulseDefinitionProperty]
    public CinemachineImpulseDefinition ImpulseDefinition = new CinemachineImpulseDefinition();

    IEnumerator _shakeCor;

    float LastEventTime = 0;

    private void Start()
    {
        StartCoroutine(ShakeCor());
    }

    public void StartShake()
    {
        if (_shakeCor == null)
        {
            StartCoroutine(ShakeCor());
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