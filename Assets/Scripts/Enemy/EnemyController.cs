using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class EnemyController : Entity
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

    public bool _isDie;

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

    [HideInInspector] public EnemyUI _enemyUI;
    public int _eventDamage;        // 받은 데미지

    public Action _dieEvent;

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
        if(EnemyType != EnemyType.Boss)
        {
            _stateMachine = new StateMachine<EnemyController>(this, new EnemyIdleState());
            _stateMachine.AddState(new EnemyDieState());

        }
        else
        {
            _stateMachine = new StateMachine<EnemyController>(this, new BossEnemyIdleState());
            //_stateMachine.AddState(new BossEnemyDieState()); 추후 보스 State 설정 시에 추가
        }

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

        _dieEvent += GearDrop;

    }


    protected virtual void Update()
    {
        _stateMachine.Update(Time.deltaTime);

    }

    public R ChangeState<R>() where R : State<EnemyController>
    {

        return _stateMachine.ChangeState<R>();
    }

    /// <summary>
    /// animation event clip 실행 마지막 함수
    /// </summary>
    public void EndAnimEvent()
    {
        ChangeState<EnemyIdleState>();
    }

    public override void Hit(float damage, GameObject attacker)
    {
        if (_isDie) return;

        _eventDamage = (int)damage;

        if(EnemyType != EnemyType.Boss)     // 보스는 hit 경직이 되지 않는다
            ChangeState<EnemyHitState>();
    }

    /// <summary>
    /// animation clip 실행 event
    /// </summary>
    public virtual void DieEvent()
    {
        Die();
    }

    public override void Die()
    {
        _dieEvent.Invoke();
        Destroy(gameObject);
    }

    /// <summary>
    /// 기어 드랍 함수 
    /// _dieEvent에 추가하여 호출
    /// </summary>
    private void GearDrop()
    {
        GearManager.Instance.AddGear(UnityEngine.Random.Range(2, 4));
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    public abstract void Attack();

}

