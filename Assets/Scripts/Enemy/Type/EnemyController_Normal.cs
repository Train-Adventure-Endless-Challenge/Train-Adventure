using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Normal : EnemyController
{
    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDeadState());
    }
}
