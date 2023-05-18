using System.Collections;
using UnityEngine;

/// <summary>
/// 플레이어의 공격을 담당하는 클래스
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    #region Variable

    [SerializeField] private float slowSpeedScale;
    [SerializeField] private float originSpeedScale;

    [SerializeField] private Transform _weaponTr;

    private float _animTime = 0.5f;

    private Weapon _weapon;
    private Player _player;
    private Animator _animator;
    private TrailRenderer _trailRenderer;
    private BoxCollider _weaponBoxCollider;
    private PlayerController _playerController;

    private IEnumerator _attackCor;

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init();
    }

    #endregion

    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _trailRenderer = _weaponTr.GetComponent<TrailRenderer>();
        _weaponBoxCollider = _weaponTr.GetComponent<BoxCollider>();
        //_weapon = _weaponTr.GetComponent<Weapon>();
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    /// <param name="isPC">플랫폼 테스트용 변수 ※추후 삭제※</param>
    public void Attack(bool isPC)
    {
        if (_attackCor == null)
        {
            if ((isPC && Input.GetMouseButtonDown(0)) || !isPC)
            {
                _player.playerState = PlayerState.Attack;
                _attackCor = AttackCor();
                StartCoroutine(AttackCor());
            }
        }
    }

    /// <summary>
    /// 공격 코루틴 함수
    /// <br/> 애니메이션, trail, State 관리
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackCor()
    {
        _animator.SetBool("IsAttack", true);
        _animator.SetTrigger("OnState");
        _trailRenderer.enabled = true;
        _weaponBoxCollider.enabled = true;
        _playerController.ChangeSlowSpeed(slowSpeedScale, _animTime);
        yield return new WaitForSeconds(_animTime);
        _playerController.ChangeSlowSpeed(originSpeedScale, _animTime);
        _trailRenderer.enabled = false;
        _weaponBoxCollider.enabled = false;
        _animator.SetBool("IsAttack", false);
        if (_playerController.moveSpeedScale <= 0f) _player.playerState = PlayerState.Idle;
        else _player.playerState = PlayerState.Move;
        _animator.SetTrigger("OnState");
        _attackCor = null;
    }

    #endregion
}
