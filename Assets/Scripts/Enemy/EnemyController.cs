using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private StateMachine<EnemyController> _stateMachine;


    void Start()
    {
        _stateMachine = new StateMachine<EnemyController>(this, new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
