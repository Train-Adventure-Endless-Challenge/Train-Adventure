using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Scientist : EnemyController
{
    [Header("Attack")]
    public Transform _attackTransform;
    [SerializeField] GameObject _bulletPrefab;
    public Transform _bombTransform;
    [SerializeField] GameObject _bombPrefab;

    PlayerManager _player => PlayerManager.Instance;

    float _skillDelayTime = 15f;
    float _timer = 0f;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyAttackWalkState());
        _stateMachine.AddState(new EnemyHitState());
    }


    protected override void Update()
    {
        base.Update();
        _timer += Time.deltaTime;
    }

    /// <summary>
    /// 쿨타임에 따라 공격 스킬일지 일반 공격일지 판단
    /// </summary>
    public void CheckAttack()
    {
        _isCurrentAttackCor = true;

        if (_timer >= _skillDelayTime)
        {
            StartCoroutine(AttackSkillCor());
            _anim.SetInteger("AttackInt", 1);   
        }
        else
        {
            StartCoroutine(AttackCor());
            _anim.SetInteger("AttackInt", 0);
        }
    }

    IEnumerator AttackSkillCor()
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

        _agent.isStopped = false;
        _isCurrentAttackCor = false;
        _timer = 0;         // 공격 스킬이 끝났을 때

    }

    /// <summary>
    /// animation clip event로 폭탄 생성
    /// </summary>
    public void SpawnBombEvent()
    {
        GameObject go = InsBullet(_bombPrefab);
        go.transform.position = _bombTransform.position;

        EnemySpawnBomb _enemyBomb = go.GetComponent<EnemySpawnBomb>();
        _enemyBomb._damage = Damage;
        _enemyBomb.Owner = this.gameObject;
    }


    IEnumerator AttackCor()
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

        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }

        _anim.SetTrigger("Attack"); //총알 공격 모션

        yield return new WaitForSeconds(AttackSpeed);
        _agent.isStopped = false;
        _isCurrentAttackCor = false;


    }


    /// <summary>
    /// anim clip에 따라 총알 생성
    /// </summary>
    public void SpawnBulletEvent()
    {
        Vector3 dir = _player.transform.position - transform.position;

        //공격 총알 생성
        GameObject bullet = InsBullet(_bulletPrefab);
        bullet.transform.position = _attackTransform.position;

        // damage 할당
        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        eb._damage = Damage;
        eb.Owner = gameObject;

    }

    /// <summary>
    /// 총알 또는 폭탄 생성
    /// </summary>
    public GameObject InsBullet(GameObject _go)
    {
        return Instantiate(_go);
    }

    /// <summary>
    /// 실제 공격 호출 함수
    /// </summary>
    public override void Attack()
    {
        CheckAttack();
    }

    /// <summary>
    /// Event가 종료시 Idle 상태로 돌아가는 animation event 
    /// </summary>
    public void EndAttackEvent()
    {
        ChangeState<EnemyIdleState>();
    }

    /// <summary>
    /// animation clip 실행 event
    /// </summary>
    public override void DieEvent()
    {
        Die();
    }
}