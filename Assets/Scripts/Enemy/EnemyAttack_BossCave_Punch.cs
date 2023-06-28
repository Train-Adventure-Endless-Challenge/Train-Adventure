using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_BossCave_Punch : MonoBehaviour
{
    EnemyController _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Hit(_owner.Damage, _owner.gameObject);
        }
    }
}
