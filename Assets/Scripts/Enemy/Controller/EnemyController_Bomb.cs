using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Bomb : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

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

    }
}
