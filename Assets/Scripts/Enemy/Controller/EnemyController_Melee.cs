using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController_Melee : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

    public bool _isAttackCheck = false;             // animation event clip으로 실행할 공격 체크 함수

    public TrailRenderer _attackTrail;
    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyAttackWalkState());
        _stateMachine.AddState(new EnemyHitState());
        _attackTrail = GetComponentInChildren<TrailRenderer>();
    }

    /// <summary>
    /// 근거리 공격
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackMeleeCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<EnemyAttackWalkState>();

            yield break;
        }

        _agent.isStopped = true;

        float timer = 0;
        while (timer <= 0.2f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;

            Vector3 dir = _player.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);

            yield return new WaitForEndOfFrame();

        }

        _anim.SetTrigger("Attack");

        yield return new WaitForSeconds(AttackSpeed);



    }

    /// <summary>
    /// Animation clip에 맞춰서 실행되는 event
    /// </summary>
    public void CheckHitEvent()
    {
        _attackTrail.enabled = true;
        _isAttackCheck = true;
    }

    public void CheckHitEndEffect()
    {
        _attackTrail.enabled = false;
        _isAttackCheck = false;
    }

    /// <summary>
    ///  공격 anmation이 끝났을때 clip event
    /// </summary>
    public void EndAnmationEvent()
    {
        _agent.isStopped = false;
        _isCurrentAttackCor = false;
        ChangeState<EnemyIdleState>();
    }

    public override void DieEvent()
    {
        base.DieEvent();
    }

    /// <summary>
    /// 공격 함수 호출
    /// </summary>
    public override void Attack()
    {
        StartCoroutine(AttackMeleeCor());
    }
}
