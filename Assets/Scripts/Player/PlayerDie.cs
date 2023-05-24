using UnityEngine;
using UnityEngine.Events;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnDie;

    private Animator _animator;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _animator = GetComponent<Animator>();
    }

    public void Die()
    {
        _animator.SetBool("IsDie", true);
        _OnDie.Invoke();
    }
}
