using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine<T> 
{
    private State<T> _currentState;     // ���� State
    private State<T> _previousState;    // ���� State
    private float _elapsedTimeInState;  // State ��ȿ �ð� 
    private Dictionary<System.Type, State<T>> _states = new Dictionary<System.Type, State<T>>();    //���� ����
    public State<T> CurrentState { get; }
    public State<T> PreviousState { get; }
    public float ElapsedTimeInState { get; }

    /// <summary>
    /// State dictionary�� �ʱ�ȭ
    /// </summary>
    /// <param name="state"></param>
    public void AddState(State<T> state)
    {

    }

    public void Update(float deltaTime)
    {
            
    }

    /// <summary>
    /// ���� ���� 
    /// </summary>
    /// <param name="state">������ ����</param>
    public void ChangeState(State<T> state)
    {

    }
}
