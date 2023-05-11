using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    protected StateMachine<EnemyController> _stateMachine;
    protected EnemyFieldOfView _enemyFieldOfView;

    protected NavMeshAgent _agent;

    protected virtual void Awake()
    {
        _enemyFieldOfView = GetComponent<EnemyFieldOfView>();
        _agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        _stateMachine = new StateMachine<EnemyController>(this, new EnemyIdleState());

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _stateMachine.Update(Time.deltaTime);
    }

    public R ChangeState<R>() where R : State<EnemyController>
    {

        return _stateMachine.ChangeState<R>();
    }

}

