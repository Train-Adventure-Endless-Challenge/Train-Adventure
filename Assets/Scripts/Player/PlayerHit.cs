using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private float _waitTime = 0.5f;

    [SerializeField] private UnityEvent _OnHit;
    [SerializeField] private UnityEvent _OnStopHit;

    private Player _player;
    private Animator _animator;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    public void Hit(float damage)
    {
        _player.Hp -= damage - ((damage / 2) * (_player.Defense / 100));
        if (_player.Hp <= 0)
        {
            Die();
            return;
        }
        _player.playerState = PlayerState.Hit;
        StartCoroutine(HitCor());
    }

    private IEnumerator HitCor()
    {
        _animator.SetBool("IsHit", true);
        _OnHit.Invoke();

        yield return new WaitForSeconds(_waitTime);

        _OnStopHit.Invoke();
        _player.playerState = PlayerState.Idle;
        _animator.SetBool("IsHit", false);
    }

    public void Die()
    {

    }
}
