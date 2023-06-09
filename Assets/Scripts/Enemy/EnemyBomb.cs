using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 5f;
    [SerializeField] private float _radius;     // 공격 적용 범위

    public int _damage;

    void Start()
    {
        StartCoroutine(Attack());
    }


    /// <summary>
    /// 효과 적용 대기 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(_attackDelay);      // 일정 시간 대기

        //터지는 효과 필요

        ShakeManager.Instance.ShakeAmount++;

        Collider[] col = Physics.OverlapSphere(transform.position, _radius, 1 << 8);        // Player Layer 8번째.
        if(col.Length > 0)
        {
            col[0].gameObject.GetComponent<PlayerHit>().Hit(_damage);
        }

        yield return new WaitForSeconds(0.2f);      // 약간 기다림 (임시)
        Destroy(gameObject);
    }

}
