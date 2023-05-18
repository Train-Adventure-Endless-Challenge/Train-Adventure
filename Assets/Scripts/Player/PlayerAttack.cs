using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _trailRenderer = _weaponTr.GetComponent<TrailRenderer>();
        _weaponBoxCollider = _weaponTr.GetComponent<BoxCollider>();
        //_weapon = _weaponTr.GetComponent<Weapon>();
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && _attackCor == null)
        {
            StartCoroutine(AttackCor());
        }
    }

    private IEnumerator AttackCor()
    {
        _attackCor = AttackCor();
        _player.playerState = PlayerState.Attack;
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
}
