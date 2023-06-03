using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 부숴지는 나무 오브젝트
/// </summary>
public class WoodObject : ObjectInTrain
{
    public override void Hit(float damage, GameObject attacker)
    {
        Die();
    }

    [ContextMenu("OnDie")]
    public override void Die()
    {
        Destroy(gameObject);
    }
    public override void ActivateByShaking()
    {
    }
}
