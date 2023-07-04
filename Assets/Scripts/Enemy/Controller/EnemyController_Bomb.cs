using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyController_Bomb : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

    [SerializeField] GameObject _bombEffect;        //폭탄이 터질때 effect 파티클

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyAttackWalkState());
        _stateMachine.AddState(new EnemyHitState());
    }

    public override void Attack()
    {
        _anim.SetTrigger("Attack");
    }

    /// <summary>
    /// 폭탄 터짐
    /// animation event
    /// </summary>
    public void AttackEvent()
    {
        _isCurrentAttackCor = true;

        if (Vector2.Distance(transform.position, _player.transform.position) > 5)
            _player.GetComponent<Player>().Hit(Damage, gameObject);

        // 파티클 추가
        Instantiate(_bombEffect, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// 공격 실행시 죽음 
    /// </summary>
    public void EndAnimationEvent()
    {

        ChangeState<EnemyDieState>();
    }
}