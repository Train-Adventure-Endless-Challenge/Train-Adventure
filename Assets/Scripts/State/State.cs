using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    protected StateMachine<T> _stateMacine;
    protected T _context;           // �۾��Ǵ� controller

    /// <summary>
    /// �ʱ�ȭ �� �� 
    /// </summary>
    public virtual void OnInitialzed()
    { }

    /// <summary>
    /// ���°� �������� �� 
    /// </summary>
    public virtual void OnEnter()
    { }

    public abstract void Update(float deltaTime);

    /// <summary>
    /// ���¸� �������� ��
    /// </summary>
    public virtual void OnExit()
    { }
}