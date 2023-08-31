using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyController_Bomb : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

    [SerializeField] GameObject _bombEffect;        //폭탄이 터질때 effect 파티클

    bool _isAttack = false;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyAttackWalkState());
        _stateMachine.AddState(new EnemyDiscoveryState());
        _stateMachine.AddState(new EnemyHitState());
    }

    public override void Attack()
    {
        if (_isAttack == true)
        {
            return;
        }

        _isAttack = true;
        _anim.SetTrigger(_attackId);
    }

    /// <summary>
    /// 폭탄 터짐
    /// animation event
    /// </summary>
    public void AttackEvent()
    {
        _isCurrentAttackCor = true;

        if (Vector2.Distance(transform.position, _player.transform.position) < 5)
            _player.GetComponent<Player>().Hit(Damage, gameObject);

        // 파티클 추가
        GameObject go = Instantiate(_bombEffect, transform.position, Quaternion.identity);
        Destroy(go,2f);
    }

    /// <summary>
    /// 공격 실행시 죽음 
    /// </summary>
    public void EndAnimationEvent()
    {
        ChangeState<EnemyDieState>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

}
