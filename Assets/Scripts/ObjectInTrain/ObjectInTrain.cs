using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기차 내에 있는 오브젝트 상위 클래스
/// </summary>
public abstract class ObjectInTrain : Entity
{
    /// <summary>
    /// 오브젝트가 흔들림에 따른 효과를 실행 시킬 흔들림 수치
    /// </summary>
    [SerializeField] private int _activateShakingCondition;
    private bool _isActivate;

    public override void Die()
    {
    }
    public override void Hit(float damage, GameObject attacker)
    {
    }

    /// <summary>
    /// 흔들림에 따른 활동
    /// </summary>
    public abstract void ActivateByShaking();

    /// <summary>
    /// 흔들림 효과 발동이 부합하는 지 체크하는 함수
    /// </summary>
    public void ShakingCheck()
    {
        if(_isActivate == false && ShakeManager.Instance.ShakeAmount > _activateShakingCondition)
        {
            ActivateByShaking();
            _isActivate = true;
        }
    }


    protected virtual void Update()
    {
        ShakingCheck();
    }
    
}
