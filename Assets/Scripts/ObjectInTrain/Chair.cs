using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Entity
{

    protected override void Start()
    {
        _maxhp = 10;
        base.Start();
    }

    public override void Hit(float damage, GameObject attacker)
    {
        Hp -= damage;

        if(Hp <= MaxHp/2)           // 반피의 데미지를 입은 상태라면
        {
            // 모델 체크 

        }

        if(Hp <= 0 )
        {
            Die();
            return;
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
