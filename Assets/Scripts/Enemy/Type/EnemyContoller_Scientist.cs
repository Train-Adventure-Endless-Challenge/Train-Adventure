using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller_Scientist : EnemyController
{
    [SerializeField] float _attackDelayTime = 10.0f;
    float _timer = 0f;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
    }

    /// <summary>
    /// Skill 일지 일반 공격일지 체크하는 함수
    /// </summary>
    public void SkillCheck()
    {
        _isCurrentAttackCor = true;
        if(_timer <= _attackDelayTime)
        {
            StartCoroutine(AttackSkill());
        }
        else
        {
            StartCoroutine(Attack());
        }
    }

    /// <summary>
    /// 스킬 공격
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackSkill()
    {
        yield return null;
    }

    /// <summary>
    /// 일반 공격
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        yield return null;
    }
}
