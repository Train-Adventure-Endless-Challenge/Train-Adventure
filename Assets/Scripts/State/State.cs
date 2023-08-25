using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    protected StateMachine<T> _stateMacine;
    protected T _context;           // 작업되는 controller

    /// <summary>
    /// 상태 set
    /// </summary>
    public void SetMachineAndContext(StateMachine<T> stateMachine, T context)
    {
        this._stateMacine = stateMachine;
        this._context = context;
    }

    /// <summary>
    /// 초기화 할 때 
    /// </summary>
    public virtual void OnInitialzed()
    { }

    /// <summary>
    /// 상태가 시작했을 때 
    /// </summary>
    public virtual void OnEnter()
    { }

    public virtual void Update(float deltaTime)
    {

    }

    /// <summary>
    /// 상태를 종료했을 때
    /// </summary>
    public virtual void OnExit()
    { }
}