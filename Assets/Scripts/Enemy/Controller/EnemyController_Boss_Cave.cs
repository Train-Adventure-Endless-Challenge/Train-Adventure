using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Boss_Cave : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new BossEnemyAttackState());
        _stateMachine.AddState(new BossEnemyAttackWalkState());


    }


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

        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }

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


        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }

        _anim.SetTrigger("Attack");

        yield return new WaitForSeconds(AttackSpeed);
    }

    /// <summary>
    /// 점프 후 플레이어 공격 데미지 
    /// animation event clip 중간 점프 바로 후에 실행
    /// </summary>
    public void JumpAttackEvent()
    {

    }

    /// <summary>
    /// 점프 공격 종료
    /// animation event clip 종료시 실행 이벤트
    /// </summary>
    public void JumpAttackAndEvent()
    {
        _isCurrentAttackCor = false;
        ChangeState<BossEnemyIdleState>();
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

        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }

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
