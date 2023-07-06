using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    range,          // 원거리
    melee,          // 근거리

    scientist,      // 미친과학자
    Bomb,           // 폭탄

    Boss            // 보스


}

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue - 9)]
public class EnemyData : ScriptableObject
{
    public string _name;
    public float _hp;
    public float _damage;
    public float _moveSpeed;
    public float _attackSpeed;
    public float _attackRange;
    public EnemyType _enemyType;
    public AudioClip _enemyDieSound;
}
