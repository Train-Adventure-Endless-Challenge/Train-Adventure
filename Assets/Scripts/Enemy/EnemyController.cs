using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    private StateMachine<EnemyController> _stateMachine;
    private EnemyFieldOfView _enemyFieldOfView;

    private void Awake()
    {
        _enemyFieldOfView = GetComponent<EnemyFieldOfView>();
    }

    void Start()
    {
        _stateMachine = new StateMachine<EnemyController>(this, new EnemyIdleState());
    }

    // Update is called once per frame
    void Update()
    {

    }

}
