using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine<T> 
{
    private T _context;                 // 상태관리를 할 컨트롤러

    private State<T> _currentState;     // 현재 State
    private State<T> _previousState;    // 이전 State
    private float _elapsedTimeInState;  // State 유효 시간 

    private Dictionary<System.Type, State<T>> _states = new Dictionary<System.Type, State<T>>();    //상태 모음
    public State<T> CurrentState { get; }
    public State<T> PreviousState { get; }
    public float ElapsedTimeInState { get; }

    public StateMachine(T context,State<T> initialState)
    {
        this._context = context;
        AddState(initialState);
        _currentState = initialState;
        _currentState.OnEnter();
    }

    /// <summary>
    /// State dictionary에 초기화
    /// </summary>
    /// <param name="state"></param>
    public void AddState(State<T> state)
    {
        state.SetMachineAndContext(this,_context);
        _states[state.GetType()] = state;
    }

    public void Update(float deltaTime)
    {
         _elapsedTimeInState += deltaTime;
        _currentState.Update(deltaTime);
    }


    /// <summary>
    /// 상태 변경 
    /// </summary>
    public R ChangeState<R>() where R : State<R>
    {
        var newType = typeof(R);


        if(CurrentState.GetType() == newType)
        {
            return _currentState as R;
        }
        if (_currentState!= null)
        {
            _currentState.OnExit();
        }

        _previousState = _currentState;
        _currentState = _states[newType];
        _currentState.OnEnter();

        _elapsedTimeInState = 0.0f;

        return _currentState as R;
    }
}
