// 작성자 : 박재만
// 작성일 : 2023-05-03

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// 플레이어의 데이터를 담고 있는 클래스
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// 스크립터블 오브젝트로 저장 되어 있는 플레이어 데이터 원본 값
    /// </summary>
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// 플레이어의 상태를 저장하는 Enum 값
    /// </summary>
    public PlayerState playerState;

    public UnityEvent _OnHit;
    public UnityEvent _OnStopHit;

    [SerializeField] private float _waitTime = 0.5f;

    private float _hp;
    private float _speed;
    private float _damage;
    private float _strength;
    private float _attackSpeed;
    private int _mp;
    private int _defense;

    public float Hp { get { return _hp; } set { _hp = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float Strength { get { return _strength; } set { _strength = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 데이터를 초기화 하는 함수
    /// <br/>
    /// 각각의 데이터는 프로퍼티를 통해 접근 가능
    /// </summary>
    private void Init()
    {
        _hp = playerData.Hp;
        _speed = playerData.Speed;
        _damage = playerData.Damage;
        _strength = playerData.Strength;
        _attackSpeed = playerData.AttackSpeed;
        _mp = playerData.Mp;
        _defense = playerData.Defense;
    }

    public void Hit(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(HitCor());
    }

    private IEnumerator HitCor()
    {
        _OnHit.Invoke();
        yield return new WaitForSeconds(_waitTime);
        _OnStopHit.Invoke();
    }

    public void Die()
    {

    }
}
