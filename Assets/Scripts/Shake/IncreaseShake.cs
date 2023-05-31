using UnityEngine;
using Cinemachine;
using System.Collections;

public class IncreaseShake : MonoBehaviour
{
    [SerializeField] private float _waitTime = 3.0f;
    [SerializeField] private float _maxValue = 10.0f;

    IEnumerator _IncreaseShakeCor;

    CinemachineImpulseDefinition _impulseDefinition;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _impulseDefinition = GetComponent<Shake>()._impulseDefinition;
    }

    public void StartIncreaseShake()
    {
        if (_IncreaseShakeCor == null)
        {
            StartCoroutine(IncreaseShakeCor());
        }
    }

    public void StopIncreaseShake()
    {
        StopCoroutine(IncreaseShakeCor());
    }

    private IEnumerator IncreaseShakeCor()
    {
        _IncreaseShakeCor = IncreaseShakeCor();
        while (true)
        {
            yield return new WaitForSeconds(_waitTime);
            if (_impulseDefinition.m_AmplitudeGain < _maxValue)
            {
                _impulseDefinition.m_AmplitudeGain++;
            }
        }
    }
}
