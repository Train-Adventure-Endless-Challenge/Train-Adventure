using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chair : Entity
{
    [SerializeField] List<GameObject> _models= new List<GameObject>(); // 완전한 의차, 반파된 의자, 파괴된 의자.
    [SerializeField] List<Collider> _colliders = new List<Collider>();
    [SerializeField] NavMeshObstacle _navMeshObstacle;
    int brokenIdx = 0;
    protected override void Start()
    {
        _maxhp = 2;
        base.Start();
    }

    public override void Hit(float damage, GameObject attacker)
    {
        Hp -= 1;

        _models[brokenIdx].SetActive(false);
        _colliders[brokenIdx].enabled = false;

        brokenIdx++;

        _models[brokenIdx].SetActive(true);
        if (_colliders.Count > brokenIdx) _colliders[brokenIdx].enabled = true;

        if (_hp <= 0) Die();
    }

    public override void Die()
    {
        _navMeshObstacle.enabled = false;
    }
}
