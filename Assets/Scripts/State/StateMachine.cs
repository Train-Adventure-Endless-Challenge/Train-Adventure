using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine<T> 
{
    private T _context;                 // ���°����� �� ��Ʈ�ѷ�

    private State<T> _currentState;     // ���� State
    private State<T> _previousState;    // ���� State
    private float _elapsedTimeInState;  // State ��ȿ �ð� 

    private Dictionary<System.Type, State<T>> _states = new Dictionary<System.Type, State<T>>();    //���� ����
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
    /// State dictionary�� �ʱ�ȭ
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
    /// ���� ���� 
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
