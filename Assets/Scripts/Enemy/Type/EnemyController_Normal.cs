using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Normal : EnemyController
{
    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDeadState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
    }

    protected override void Update()
    {
        base.Update();
    }
}
