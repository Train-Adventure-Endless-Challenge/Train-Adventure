using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine<T> 
{
    private State<T> _currentState;     // 현재 State
    private State<T> _previousState;    // 이전 State
    private float _elapsedTimeInState;  // State 유효 시간 
    private Dictionary<System.Type, State<T>> _states = new Dictionary<System.Type, State<T>>();    //상태 모음
    public State<T> CurrentState { get; }
    public State<T> PreviousState { get; }
    public float ElapsedTimeInState { get; }

    /// <summary>
    /// State dictionary에 초기화
    /// </summary>
    /// <param name="state"></param>
    public void AddState(State<T> state)
    {

    }

    public void Update(float deltaTime)
    {
            
    }

    /// <summary>
    /// 상태 변경 
    /// </summary>
    /// <param name="state">변경할 상태</param>
    public void ChangeState(State<T> state)
    {

    }
}
