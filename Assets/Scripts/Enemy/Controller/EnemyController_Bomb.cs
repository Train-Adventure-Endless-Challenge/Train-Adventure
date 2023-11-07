using UnityEngine;
using System.Collections;

public class EnemyController_Bomb : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

    [SerializeField] GameObject _bombEffect;        //폭탄이 터질때 effect 파티클
    [SerializeField] LayerMask targetLayer;

    bool _isAttack = false;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyAttackWalkState());
        _stateMachine.AddState(new EnemyDiscoveryState());
        _stateMachine.AddState(new EnemyHitState());

        StartCoroutine(OnCollisionPlayer());
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

        if (Vector2.Distance(transform.position, _player.transform.position) < 1.6f)
            _player.GetComponent<Player>().Hit(Damage, gameObject);

        // 파티클 추가
        GameObject go = Instantiate(_bombEffect, transform.position, Quaternion.identity);
        Destroy(go, 2f);
    }

    /// <summary>
    /// 공격 실행시 죽음 
    /// </summary>
    public void EndAnimationEvent()
    {
        ChangeState<EnemyDieState>();
    }

    /// <summary>
    /// 플레이어와의 충돌을 감지하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator OnCollisionPlayer()
    {
        while (Physics.OverlapSphere(transform.position, AttackRange, targetLayer).Length == 0) // 플레이어를 감지할 때 까지 대기
        {
            yield return new WaitForEndOfFrame();
        }
        Attack(); // 공격 실행
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, AttackRange);
    //}
}
