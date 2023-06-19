using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Scientist : EnemyController
{

    PlayerManager _player => PlayerManager.Instance;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
        _stateMachine.AddState(new EnemyAttackWalkState());
        _stateMachine.AddState(new EnemyHitState());
    }
}
