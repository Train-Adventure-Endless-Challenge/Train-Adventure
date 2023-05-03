using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    range,      // 원거리
    melee       // 근거리
}

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue - 9)]
public class EnemyData : ScriptableObject
{
    private string _name;
    private float _hp;
    private float _damage;          
    private float _moveSpeed;       
    private float _attackSpeed;
    private float _attackRange;
    private EnemyType _enemyType;

    public string Name { get { return _name; } }
    public float HP { get { return _hp; } }
    public float Damage { get { return _damage; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackRange { get { return _attackRange; } }
    public EnemyType EnemyType { get { return EnemyType; } }
}
