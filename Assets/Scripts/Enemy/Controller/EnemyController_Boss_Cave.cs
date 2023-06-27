using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController_Boss_Cave : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;


    public override void Attack()
    {
        CheckAttack();
    }

    void CheckAttack()
    {
        int num = Random.Range(0, 101);
        switch (num / 10)           // 정해진 확률에 따른 공격 패턴 변경
        {
            case 0:
            case 1:
            case 2:
            case 3:
                SpawnBombAttack();
                break;
            case 4:
            case 5:
            case 6:
            case 7:
                JumpAttack();
                break;
            case 8:
            case 9:
            case 10:
                PunchAttack();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 폭탄 생성 공격
    /// </summary>
    private void SpawnBombAttack()
    {
        Debug.Log("폭탄 생성");

        _anim.SetInteger("AttackInt", 0);
        StartCoroutine(SpawnBombAttackCor());
    }

    /// <summary>
    /// 점프 공격
    /// </summary>
    private void JumpAttack()
    {
        Debug.Log("점프 공격");

        _anim.SetInteger("AttackInt", 1);
        StartCoroutine(JumpAttackCor());
    }

    private void PunchAttack()
    {
        Debug.Log("펀치 공격");

        _anim.SetInteger("AttackInt", 2);
        StartCoroutine(PunchAttackCor());
    }

    IEnumerator SpawnBombAttackCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<BossEnemyAttackWalkState>();

            yield break;
        }

        _agent.isStopped = true;

        // animation 실행

        yield return new WaitForSeconds(AttackSpeed);

        
    }

    IEnumerator JumpAttackCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<BossEnemyAttackWalkState>();

            yield break;
        }

        _agent.isStopped = true;

        yield return new WaitForSeconds(AttackSpeed);
    }

    IEnumerator PunchAttackCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<BossEnemyAttackWalkState>();

            yield break;
        }

        _agent.isStopped = true;

        yield return new WaitForSeconds(AttackSpeed);
    }

    /// <summary>
    ///  공격 anmation이 끝났을때 clip event
    /// </summary>
    public void EndAnmationEvent()
    {
        _agent.isStopped = false;
        _isCurrentAttackCor = false;
        ChangeState<BossEnemyIdleState>();
    }

    /// <summary>
    /// animation clip 실행 event
    /// </summary>
    public override void DieEvent()
    {
        base.DieEvent();
    }
}
