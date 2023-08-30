using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Entity
{
    [SerializeField] List<GameObject> models= new List<GameObject>(); // 완전한 의차, 반파된 의자, 파괴된 의자.
    [SerializeField] List<Collider> colliders = new List<Collider>();
    int brokenIdx = 0;
    protected override void Start()
    {
        _maxhp = 2;
        base.Start();
    }

    public override void Hit(float damage, GameObject attacker)
    {
        Hp -= 1;

        models[brokenIdx].SetActive(false);
        colliders[brokenIdx].enabled = false;

        brokenIdx++;

        models[brokenIdx].SetActive(true);
        if (colliders.Count > brokenIdx) colliders[brokenIdx].enabled = true;

    }

    public override void Die()
    {
    }
}
