// 작성자 : 박재만
// 작성일 : 2023-06-19

#region Namespace

using UnityEngine;
using UnityEngine.Events;

#endregion

/// <summary>
/// 플레이어 죽음을 담당하는 클래스
/// </summary>
public class PlayerDie : MonoBehaviour
{
    #region Variable

    [SerializeField] private UnityEvent _OnDie;

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
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 죽음 함수
    /// <br/>
    /// 유니티 이벤트를 통해 인스펙터에서 해결
    /// </summary>
    public void Die()
    {
        _animator.SetBool("IsDie", true);
        _OnDie.Invoke();
    }

    #endregion
}
