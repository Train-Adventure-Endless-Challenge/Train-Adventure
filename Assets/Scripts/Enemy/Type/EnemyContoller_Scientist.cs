using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller_Scientist : EnemyController
{
    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new EnemyDieState());
        _stateMachine.AddState(new EnemyMoveState());
        _stateMachine.AddState(new EnemyAttackState());
    }

    /// <summary>
    /// Skill 일지 일반 공격일지 체크하는 함수
    /// </summary>
    public void SkillCheck()
    {

    }


}
