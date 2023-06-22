using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    float _attackDelayTime = 10f;
    public float _damage;
    public GameObject _owner;

    [SerializeField] GameObject _effectPrefab;      // 폭탄이 터지는 effect
    void Start()
    {
        StartCoroutine(AttackCor());
    }

    IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(_attackDelayTime);      // 10초 후에

        // 폭탄 터짐 
        Collider[] collider = Physics.OverlapSphere(transform.position, 5f);
        foreach (var item in collider)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.GetComponent<Player>().Hit(_damage,_owner); break;
            }
        }

        // 폭파되는 파티클 생성
        GameObject effect = Instantiate(_effectPrefab,transform.position,Quaternion.identity);
        Destroy(effect,2f);
        Destroy(gameObject);

    }
}
