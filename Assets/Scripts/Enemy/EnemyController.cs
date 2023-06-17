using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    protected StateMachine<EnemyController> _stateMachine;
    public Animator _anim;

    [Header("EnemyData")]
    [SerializeField] private EnemyData _enemyData;
    private string _name;
    private float _hp;
    private float _damage;
    private float _moveSpeed;
    private float _attackSpeed;
    private float _attackRange;
    private EnemyType _enemyType;

    public string Name { get { return _name; } }
    public float HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0)
            {
                _stateMachine.ChangeState<EnemyDieState>();
            }
        }
    }
    public float Damage { get { return _damage; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public EnemyType EnemyType { get { return _enemyType; } }

    [Header("Move")]
    [HideInInspector] public NavMeshAgent _agent;
    public Transform _target;

    [Header("AttackCheck")]
    [HideInInspector] public EnemyFieldOfView _enemyFieldOfView;
    public bool _isCurrentAttackCor;

    EnemyUI _enemyUI;


    protected virtual void Awake()
    {
        _enemyFieldOfView = GetComponent<EnemyFieldOfView>();
        _agent = GetComponentInChildren<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        _enemyUI = GetComponent<EnemyUI>();
    }

    protected virtual void Start()
    {
        Init();
        _stateMachine = new StateMachine<EnemyController>(this, new EnemyIdleState());
        _stateMachine.AddState(new EnemyDieState());
    }

    /// <summary>
    /// enemy 초기화
    /// </summary>
    protected virtual void Init()
    {
        _name = _enemyData._name;
        _hp = _enemyData._hp;
        _damage = _enemyData._damage;
        _moveSpeed = _enemyData._moveSpeed;
        _attackSpeed = _enemyData._attackSpeed;
        _attackRange = _enemyData._attackRange;
        _enemyType = _enemyData._enemyType;

        _agent.stoppingDistance = AttackRange;

    }


    protected virtual void Update()
    {
        _stateMachine.Update(Time.deltaTime);


    }

    public R ChangeState<R>() where R : State<EnemyController>
    {

        return _stateMachine.ChangeState<R>();
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        _anim.SetTrigger("Hit");
        HP -= damage;
        _enemyUI._hpBarSlider.value = HP;

    }

    /// <summary>
    /// Animation clip으로 찾기
    /// </summary>
    public void ChangeIdleEvent()
    {
        _stateMachine.ChangeState<EnemyIdleState>();        // 맞았을시 IDle State

    }
}

