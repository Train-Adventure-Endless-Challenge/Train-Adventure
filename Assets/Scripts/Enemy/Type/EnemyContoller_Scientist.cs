using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller_Scientist : EnemyController
{
    [SerializeField] float _attackDelayTime = 10.0f;
    float _skillTimer = 0f;
    private bool _isSkill;      

    [Header("Attack")]
    public Transform _attackTransform;
    [SerializeField] GameObject _bulletPrefab;      // 일반 총알 프리팹
    public Transform _bombTransform;
    [SerializeField] GameObject _bombPrefab;        // 폭탄 프리팹


    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
    }

    protected override void Update()
    {
        base.Update();

        if (_isSkill)
            _skillTimer += Time.deltaTime;

    }

    /// <summary>
    /// Skill 일지 일반 공격일지 체크하는 함수
    /// </summary>
    public void SkillCheck()
    {
        _isCurrentAttack = true;
        if(_skillTimer >= _attackDelayTime)
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
        _isSkill = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > AttackRange + _agent.stoppingDistance)
        {
            _isCurrentAttack = false;
            yield break;
        }

        _isCurrentAttack = true;
        _agent.isStopped = true;

        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 0.5f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);

            yield return new WaitForEndOfFrame();

        }

        // 특수 공격 모션 필요

        // 공격 (폭탄 생성)
        GameObject bombObj = Instantiate(_bombPrefab, _bombTransform.position, Quaternion.identity);        
        bombObj.GetComponent<EnemyBomb>()._damage = (int)Damage;

        yield return new WaitForSeconds(AttackSpeed);
        _agent.isStopped = false;
        _isCurrentAttack = false;

        _isSkill = false;
        _skillTimer = 0;

    }

    /// <summary>
    /// 일반 공격
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > AttackRange + _agent.stoppingDistance)
        {
            _isCurrentAttack = false;
            yield break;
        }

        _isCurrentAttack = true;
        _agent.isStopped = true;

        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 0.5f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);

            yield return new WaitForEndOfFrame();

        }

        _anim.SetTrigger("Attack");

        //공격
        GameObject bullet = InsBullet();
        bullet.transform.position = _attackTransform.position;
        bullet.transform.rotation = Quaternion.LookRotation(dir).normalized;

        // damage 할당
        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        eb._damage = (int)Damage;

        yield return new WaitForSeconds(AttackSpeed);
        _agent.isStopped = false;
        _isCurrentAttack = false;
    }

    /// <summary>
    /// 총알 생성
    /// </summary>
    public GameObject InsBullet()
    {
        return Instantiate(_bulletPrefab);
    }
}
