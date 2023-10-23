using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class EnemyController : Entity
{
    public StateMachine<EnemyController> _stateMachine;
    public Animator _anim;

    [Header("EnemyData")]
    [SerializeField] private EnemyData _enemyData;
    private string _name;
    private float _damage;
    private float _moveSpeed;
    private float _attackSpeed;
    private float _attackRange;

    private EnemyType _enemyType;
    private AudioClip _enemyDieSound;

    public bool _isDie;
    public bool _isHit;

    public string Name { get { return _name; } }
    public override float Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0 && !_isDie)
            {
                if (EnemyType != EnemyType.Boss)
                    _stateMachine.ChangeState<EnemyDieState>();
                else
                    _stateMachine.ChangeState<BossEnemyDieState>();
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

    protected readonly int _attackId = Animator.StringToHash("Attack");

    protected virtual void Awake()
    {
        _enemyFieldOfView = GetComponent<EnemyFieldOfView>();
        _agent = GetComponentInChildren<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        _enemyUI = GetComponent<EnemyUI>();
    }

    protected override void Start()
    {
        Init();
        base.Start();
        _enemyUI.Init();
        if (EnemyType != EnemyType.Boss)
        {
            _stateMachine = new StateMachine<EnemyController>(this, new EnemyIdleState());
            _stateMachine.AddState(new EnemyDieState());
        }
        else
        {
            _stateMachine = new StateMachine<EnemyController>(this, new BossEnemyIdleState());
            _stateMachine.AddState(new BossEnemyDieState());
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
        _enemyDieSound = _enemyData._enemyDieSound;

        _agent.stoppingDistance = AttackRange;
        _agent.speed = MoveSpeed;

        _dieEvent += GearDrop;

        if(_agent.isOnNavMesh)
        {
            _agent.ResetPath(); // 동적으로 씬 생성 시 경로를 찾지 못하는 버그를 해결하기 위해 시작 시 경로를 초기화 시켜줌
        }

        _anim.SetTrigger("Jump");
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
        if(Hp > 0)
            ChangeState<EnemyIdleState>();
    }

    public override void Hit(float damage, GameObject attacker)
    {
        if (_isDie) return;

        if (attacker.GetComponent<Player>().playerState == PlayerState.Skill)
        {
            attacker.GetComponent<PlayerSound>().PlaySkillSound();
        }
        else
        {
            attacker.GetComponent<PlayerSound>().PlayWeaponAttackSound();
        }

        _eventDamage = (int)damage;

        if (EnemyType != EnemyType.Boss)     // 보스는 hit 경직이 되지 않는다
        {
            ChangeState<EnemyIdleState>(); // 기존 Hit State로 다시 돌아가기 위한 State 초기화
            ChangeState<EnemyHitState>();
        }
        else if (EnemyType == EnemyType.Bomb) //폭탄은 맞았을때 공격한다
        {
            ChangeState<EnemyAttackState>();
        }
        else  // 보스라면 HP 처리 따로
        {
            if (Hp <= 0) return;

            Hp -= _eventDamage;
            _enemyUI.UpdateHpUI(Hp);
        }
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
        SoundManager.Instance.SFXPlay(_enemyDieSound);
        _dieEvent.Invoke();
        Destroy(gameObject.transform.parent.gameObject);
    }

    /// <summary>
    /// 기어 드랍 함수 
    /// _dieEvent에 추가하여 호출
    /// </summary>
    private void GearDrop()
    {
        if (EnemyType == EnemyType.Bomb) return;        // 폭탄 몬스터는 터져도 기어가 드랍되지 않음
        GearManager.Instance.AddGear(UnityEngine.Random.Range(2, 4));
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    public abstract void Attack();

}

