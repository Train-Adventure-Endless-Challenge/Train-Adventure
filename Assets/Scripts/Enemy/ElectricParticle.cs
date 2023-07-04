using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boss Enemy가 Punch 공격을 차징 중일때 활성화 되는 파티클
/// Animation clip에서 관리
/// </summary>
public class ElectricParticle : MonoBehaviour
{
    ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        _particle.Play();           // 파티클 오브젝트를 활성화 할때마다 파티클을 실행시켜줘야 한다
    }
}
