using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public PlayerState playerState;

    private float _hp;
    private float _speed;
    private float _damage;
    private float _strength;
    private float _attackSpeed;
    private int _mp;
    private int _defense;

    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }
    public float Damage { get { return _damage; } }
    public float Strength { get { return _strength; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public int Mp { get { return _mp; } }
    public int Defense { get { return _defense; } }

    private void Awake()
    {
        Init();
    }

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

    public void ChangeHp(float value)
    {
        _hp += value;
    }
    public void ChangeSpeed(float value)
    {
        _speed += value;
    }
    public void ChangeDamage(float value)
    {
        _damage += value;
    }
    public void ChangeStrength(float value)
    {
        _strength += value;
    }
    public void ChangeAttackSpeed(float value)
    {
        _attackSpeed += value;
    }
    public void ChangeMp(int value)
    {
        _mp += value;
    }
    public void ChangeDefense(int value)
    {
        _defense += value;
    }
    public void Hit(float damage)
    {
        _hp -= damage;
    }
    public void UseMp(int value)
    {
        _mp -= value;
    }
    public void RecoverHp(float value)
    {

    }
    public void RecoverMp(float value)
    {

    }
}
