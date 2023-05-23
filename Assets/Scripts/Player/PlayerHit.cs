using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 플레이어의 충돌을 담당하는 클래스
/// </summary>
public class PlayerHit : MonoBehaviour
{
    #region Variable

    [Header("Variable")]
    [SerializeField] private float _animTime = 0.8f; // 충돌 애니메이션 속도 (원래는 0.71초 이지만, 오류 방지를 위해 0.09초 추가 하여 사용)

    [Header("Event")]
    [SerializeField] private UnityEvent _OnHit;
    [SerializeField] private UnityEvent _OnStopHit;

    private Player _player;
    private Animator _animator;

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
    }

    /// <summary>
    /// 플레이어의 충돌 함수
    /// <br/>
    /// 공식 | 데미지 - ((데미지x(1/2))x(방어력/100)
    /// </summary>
    /// <param name="damage">데미지 량</param>
    public void Hit(float damage)
    {
        _player.Hp -= damage - ((damage / 2) * (_player.Defense / 100)); // 데미지 공식 데미지 - ((데미지x(1/2))x(방어력/100)
        if (_player.Hp <= 0)
        {
            //Die(); // ※추후 추가※
            return;
        }
        _player.playerState = PlayerState.Hit; // 플레이어의 상태 변경
        StartCoroutine(HitCor());              // 플레이어 충돌 코루틴 실행
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
        _OnStopHit.Invoke();
        _player.playerState = PlayerState.Idle;
        _animator.SetBool("IsHit", false);
    }

    #endregion
}
