using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController_Melee : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
    }

    /// <summary>
    /// 근거리 공격
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackMeleeCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > AttackRange + _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
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

        _agent.isStopped = false;

        _isCurrentAttackCor = false;

    }

    /// <summary>
    /// Animation clip에 맞춰서 실행되는 event
    /// </summary>
    public void CheckHitEvent()
    {
        Debug.Log("Hit");

        // 실제 공격 체크
        if (_enemyFieldOfView._isVisiblePlayer && Vector3.Distance(transform.position, _player.transform.position) < AttackRange + _agent.stoppingDistance)
        {
            PlayerHit player = _player.GetComponent<PlayerHit>();     //  추후 싱글톤으로 찾는다면 로직 수정
            player.Hit(Damage);
        }
    }
}
