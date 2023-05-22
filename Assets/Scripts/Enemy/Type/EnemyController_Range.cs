using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Range : EnemyController
{
    public GameObject _laserObj;

    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());

        _laserObj.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }
}
