using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Boss_Cave : EnemyController
{
    PlayerManager _player => PlayerManager.Instance;

    [SerializeField] GameObject _enemyAttackObj;        // 펀치 공격 시의 콜라이더 오브젝트

    [Header("Spawn")]
    [SerializeField] GameObject _bombPrefab;
    public GameObject _floorEffect;

    private float _shakeAmount = 3f;       // 공격시 흔들림 증가량

    [Header("Attack")]
    private readonly int _attackIntId = Animator.StringToHash("AttackInt");
    private int _attackIndex = -1;           // 공격 패턴 인덱스
    enum AttackTypeBossCave
    {
        Spawn,
        Jump,
        Punch
    }
    
    [SerializeField] private AttackTypeBossCave[] _attackPattern = new AttackTypeBossCave[] 
    { AttackTypeBossCave.Spawn, AttackTypeBossCave.Jump, AttackTypeBossCave.Jump, AttackTypeBossCave.Punch };       // 공격 패턴 (추후 기획에 따른 변경 가능)


    protected override void Start()
    {
        base.Start();

        _stateMachine.AddState(new BossEnemyAttackState());
        _stateMachine.AddState(new BossEnemyAttackWalkState());
        _stateMachine.AddState(new BossEnemyMoveState());

        _enemyAttackObj.SetActive(false);

    }

    public override void Attack()
    {
        CheckAttack();
    }

    void CheckAttack()
    {
        _attackIndex++;
        if(_attackIndex >= _attackPattern.Length )
            _attackIndex= 0;

        switch (_attackPattern[_attackIndex])
        { 
        
            case AttackTypeBossCave.Spawn:
                SpawnBombAttack();
                break;
            case AttackTypeBossCave.Jump:
                JumpAttack();
                break;
            case AttackTypeBossCave.Punch:
                PunchAttack();
            break;
            default:
                break;
        }
    }
    

    #region SpawnAttack

    /// <summary>
    /// 폭탄 생성 공격
    /// </summary>
    private void SpawnBombAttack()
    {
        _anim.SetInteger(_attackIntId, 0);
        StartCoroutine(SpawnBombAttackCor());
    }

    IEnumerator SpawnBombAttackCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<BossEnemyAttackWalkState>();

            yield break;
        }

        _agent.isStopped = true;

        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }

        // animation 실행
        _anim.SetTrigger(_attackId);

        yield return new WaitForSeconds(AttackSpeed);

        
    }

    /// <summary>
    /// animation clip event
    /// 폭탄을 생성한다 
    /// </summary>
    public void StartSpawnBombEvent()
    {
        Instantiate(_bombPrefab, _enemyAttackObj.transform.position, Quaternion.identity);      // 폭탄 왼손에 소환
    }

    #endregion

    #region JumpAttack

    /// <summary>
    /// 점프 공격
    /// </summary>
    private void JumpAttack()
    {
        _anim.SetInteger(_attackIntId, 1);
        StartCoroutine(JumpAttackCor());
    }

    IEnumerator JumpAttackCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<BossEnemyAttackWalkState>();

            yield break;
        }
        _agent.isStopped = true;


        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }

        _anim.SetTrigger(_attackId);

        yield return new WaitForSeconds(AttackSpeed);
    }

    /// <summary>
    /// 점프 후 플레이어 공격 데미지 
    /// animation event clip 중간 점프 바로 후에 실행
    /// </summary>
    public void JumpAttackEvent()
    {
        //카메라 흔들림. 추후 흔들림 메니저 관리 호출로 변경
        _player.GetComponent<CinemachineImpulseSource>().GenerateImpulse();

        // 몬스터가 바닥에 착지 했을 때 플레이어의 거리가 일정 이하(5는 임시. 추후 기획을 통해 변경)라면 
        if(Vector3.Distance(_player.transform.position, transform.position) <= 5)
        {
            _player.GetComponent<Player>().Hit(Damage, gameObject);
        }

        GameObject go =  Instantiate(_floorEffect, transform.position, Quaternion.identity);
        go.transform.localScale = new Vector3(5, 5, 1);
        Destroy(go, 1f);
    }
    #endregion

    #region PunchAttack

    /// <summary>
    /// 펀치 공격
    /// </summary>
    private void PunchAttack()
    {
        _anim.SetInteger(_attackIntId, 2);
        StartCoroutine(PunchAttackCor());
    }

    IEnumerator PunchAttackCor()
    {
        _isCurrentAttackCor = true;

        // 잘못 인식된 경우 나가기
        if (Vector3.Distance(transform.position, _player.transform.position) > _agent.stoppingDistance)
        {
            _isCurrentAttackCor = false;
            ChangeState<BossEnemyAttackWalkState>();

            yield break;
        }

        _agent.isStopped = true;

        Vector3 dir = _player.transform.position - transform.position;
        float timer = 0;
        while (timer <= 1f)       // 약간의 delay. temp 값.
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2);
            yield return new WaitForEndOfFrame();

        }

        _anim.SetTrigger(_attackId);

        yield return new WaitForSeconds(AttackSpeed);
    }

    /// <summary>
    /// animation event
    /// </summary>
    public void PunchAttackEvent()
    {
        // 펀치 오브젝트 활성화
        _enemyAttackObj.SetActive(true);
    }

    public void PunchEffectEvent()
    {
        GameObject go = Instantiate(_floorEffect, transform.position, Quaternion.identity);
        go.transform.localScale = new Vector3(5, 5, 1);
        Destroy(go, 1f);

        ShakeManager.Instance.IncreaseShake(_shakeAmount);        // 흔들림 증가
    }

    #endregion

    /// <summary>
    ///  공격 anmation이 끝났을때 clip event
    /// </summary>
    public void EndAnmationEvent()
    {

        _enemyAttackObj.SetActive(false);

        _agent.isStopped = false;
        _isCurrentAttackCor = false;
        ChangeState<BossEnemyIdleState>();
    }

    /// <summary>
    /// animation clip 실행 event
    /// </summary>
    public override void DieEvent()
    {
        base.DieEvent();
    }
}
