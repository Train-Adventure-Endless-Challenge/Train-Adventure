using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/Player Data", order = int.MaxValue - 10)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _strength;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _mp;
    [SerializeField] private int _defense;

    public float Hp { get { return _hp; } }
    public float Speed { get { return _speed; } }
    public float Damage { get { return _damage; } }
    public float Strength { get { return _strength; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public int Mp { get { return _mp; } }
    public int Defense { get { return _defense; } }
}
