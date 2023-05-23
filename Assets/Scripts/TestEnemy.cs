using Cinemachine;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private CinemachineImpulseSource _cinemachineImpulseSource;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake()
    {
        _cinemachineImpulseSource.GenerateImpulse();
    }
}
