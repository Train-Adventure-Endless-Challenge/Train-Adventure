using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Range : EnemyController
{
    [Header("Attack")]
    public GameObject _dangerLinePrefab;

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

    public GameObject CloneDangerLinePrefab()
    {
        return Instantiate(_dangerLinePrefab,Vector3.zero, Quaternion.identity);
    }
}
