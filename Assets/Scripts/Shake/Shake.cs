using UnityEngine;
using Cinemachine;
using System.Collections;

public class Shake : MonoBehaviour
{
    [CinemachineImpulseDefinitionProperty]
    public CinemachineImpulseDefinition _impulseDefinition = new CinemachineImpulseDefinition();

    float _lastEventTime = 0;

    IEnumerator _shakeCor;

    public void StartShake()
    {
        if (_shakeCor == null)
        {
            StartCoroutine(ShakeCor());
        }
    }

    public void StopShake()
    {
        StopCoroutine(ShakeCor());
    }

    private IEnumerator ShakeCor()
    {
        _shakeCor = ShakeCor();
        while (true)
        {
            var now = Time.time;
            float eventLength = _impulseDefinition.m_TimeEnvelope.m_AttackTime + _impulseDefinition.m_TimeEnvelope.m_SustainTime;
            if (now - _lastEventTime > eventLength)
            {
                _impulseDefinition.CreateEvent(transform.position, Vector3.down);
                _lastEventTime = now;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
