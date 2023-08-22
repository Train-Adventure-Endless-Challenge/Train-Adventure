using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Range : EnemyController
{
    [Header("Attack")]
    public Transform _attackTransform;
    [SerializeField] GameObject _bulletPrefab;

    PlayerManager _player => PlayerManager.Instance;

    Vector3 _dir;           // 총알 방향 위치를 위한 변수

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyDiscoveryState());
        _stateMachine.AddState(new EnemyAttackWalkState());
        _stateMachine.AddState(new EnemyHitState());
        _stateMachine.AddState(new EnemyDiscoveryState());
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 총알 생성
    /// </summary>
    public GameObject InsBullet()
    {
        return Instantiate(_bulletPrefab);
    }


    /// <summary>
    /// 원거리 공격
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackRangeCor()
    {

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<EnemyAttackWalkState>();

            yield break;
        }

        _isCurrentAttackCor = true;
        _agent.isStopped = true;

        Vector3 dir = PlayerManager.Instance.transform.position - transform.position;
        _dir = _player.transform.position + new Vector3(0, 1f, 0);            // 기존 플레이어의 위치는 바닥에 붙어 있어 총알이 바닥에 먼저 체크 되는 문제로 임시 vector 값을 더해줌
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }



        _anim.SetTrigger("Attack");

        yield return new WaitForSeconds(AttackSpeed);
        _agent.isStopped = false;
        _isCurrentAttackCor = false;

    }

    /// <summary>
    /// animation Event로 실행
    /// </summary>
    void AttackAnimEnd()
    {
        ChangeState<EnemyIdleState>();
    }

    /// <summary>
    /// anim clip에 따라 총알 생성
    /// </summary>
    public void SpawnBulletEvent()
    {

        //공격 총알 생성
        GameObject bullet = InsBullet();
        bullet.transform.position = _attackTransform.position;


        // damage 할당
        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        eb._damage = Damage;
        eb.Owner = gameObject;
        eb._dir = _dir;

    }

    /// <summary>
    /// animation clip 실행 event
    /// </summary>
    public override void DieEvent()
    {
        base.DieEvent();
    }

    /// <summary>
    /// 실제 공격 호출 함수
    /// </summary>
    public override void Attack()
    {
        StartCoroutine(AttackRangeCor());
    }

    /// <summary>
    /// Event가 종료시 Idle 상태로 돌아가는 animation event 
    /// </summary>
    public void EndAttackEvent()
    {
        ChangeState<EnemyIdleState>();
    }
}
