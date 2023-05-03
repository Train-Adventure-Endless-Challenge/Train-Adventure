using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    range,      // 원거리
    melee       // 근거리
}

public class EnemyData : ScriptableObject
{
    private string _name;
    private float _hp;
    private float _damage;          
    private float _moveSpeed;       
    private float _attackSpeed;
    private float _attackRange;
    private EnemyType _enemyType;

    public string Name { get; set; }
    public float HP { get; set; }
    public float Damage { get; set; }
    public float MoveSpeed { get; set; }
    public float AttackSpeed { get; set; }
    public float AttackRange { get; set;}
    public EnemyType EnemyType { get; set; }
}
