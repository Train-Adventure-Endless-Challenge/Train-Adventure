using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    float _attackDelayTime = 10f;
    public float _damage;
    public GameObject _owner;

    void Start()
    {
        StartCoroutine(AttackCor());
    }

    IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(_attackDelayTime);      // 10초 후에

        // 폭탄 터짐 

        Collider[] collider = Physics.OverlapSphere(transform.position, 3f,LayerMask.NameToLayer("Player"));
        if(collider.Length > 0)
        {
            Player player = collider[0].GetComponent<Player>();
            player.Hit(_damage, _owner);
        }

        // 폭파되는 파티클 생성
        
        Destroy(gameObject);

    }
}
