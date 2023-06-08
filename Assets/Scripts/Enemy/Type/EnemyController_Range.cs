using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController_Range : EnemyController
{
    [Header("Attack")]
    public Transform _attackTransform;
    [SerializeField] GameObject _bulletPrefab;

    PlayerManager _player => PlayerManager.Instance;

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
        if (Vector3.Distance(transform.position, _player.transform.position) > AttackRange + _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            yield break;
        }

        _isCurrentAttackCor = true;
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
        _isCurrentAttackCor = false;

    }
}
