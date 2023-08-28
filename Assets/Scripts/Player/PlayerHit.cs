// 작성자 : 박재만
// 작성일 : 2023-06-19

#region Namespace

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

#endregion

/// <summary>
/// 플레이어의 충돌을 담당하는 클래스
/// </summary>
public class PlayerHit : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private float _animTime = 0.4f; // 충돌 애니메이션 속도 (애니메이션 변경에 따라 수치 수정)

    [Header("Event")]
    [SerializeField] private UnityEvent _OnHit;

    #region Class

    private Player _player;
    private Animator _animator;
    private PlayerDie _playerDie;

    private Coroutine _hitCor;

    #endregion

    #endregion

    #region Function

    #region LifeCycle

    private void Awake()
    {
        Init();
    }

    #endregion

    /// <summary>
    /// 초기화 함수
    /// </summary>
    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _playerDie = GetComponent<PlayerDie>();
    }

    /// <summary>
    /// 플레이어의 충돌 함수
    /// <br/>
    /// 공식 | 데미지 - ((데미지x(1/2))x(방어력/100)
    /// </summary>
    /// <param name="damage">데미지 량</param>
    public void Hit(float damage)
    {
        if (_hitCor == null)
        {
            _player.Hp -= damage - ((damage / 2) * (_player.Defense / 100)); // 데미지 공식 데미지 - ((데미지x(1/2))x(방어력/100)
            if (_player.Hp <= 0)
            {
                _playerDie.Die();
                InGameManager.Instance.GameOver();

                return;
            }
            _player.playerState = PlayerState.Hit; // 플레이어의 상태 변경
            _hitCor = StartCoroutine(HitCor());    // 플레이어 충돌 코루틴 실행
        }
    }

    /// <summary>
    /// 플레이어 충돌 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator HitCor()
    {
        // 충돌 실행
        _animator.SetBool("IsHit", true);
        _OnHit.Invoke();

        yield return new WaitForSeconds(_animTime); // 애니메이션 시간 대기

        // 충돌 종료
        _player.playerState = PlayerState.Idle;
        _animator.SetBool("IsHit", false);
        _hitCor = null;
    }

    #endregion
}
