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

    float _skillDelayTime = 10f;
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

    public void CheckAttack()
    {
        if(_timer >= _skillDelayTime)
        {
            StartCoroutine(AttackSkill());
            _anim.SetInteger("AttackInt", 1);   
        }
        else
        {
            StartCoroutine(Attack());
            _anim.SetInteger("AttackInt", 0);
        }
    }

    IEnumerator AttackSkill()
    {
        GameObject go = InsBullet(_bombPrefab);
        go.transform.position = _bombTransform.position;

        yield return null;
        _timer = 0;         // 공격 스킬이 끝났을 때

    }


    IEnumerator Attack()
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
        GameObject bullet = InsBullet(_bulletPrefab
            
            );
        bullet.transform.position = _attackTransform.position;

        // damage 할당
        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        eb._damage = Damage;
        eb.Owner = gameObject;

    }

    /// <summary>
    /// 총알 생성
    /// </summary>
    public GameObject InsBullet(GameObject _go)
    {
        return Instantiate(_go);
    }

}
