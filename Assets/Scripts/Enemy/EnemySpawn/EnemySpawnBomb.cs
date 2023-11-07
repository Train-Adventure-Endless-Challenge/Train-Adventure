using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBomb : Entity
{
    [Header("Enemy")]
    float _attackDelayTime = 10f;
    float _increaseAmount = 1f;
    [SerializeField] float _setHp;
    public float _damage;
    EnemyUI _enemyUI;
    public GameObject Owner { get; set; }

    
    [SerializeField] GameObject _effectPrefab;      // 폭탄이 터지는 effect
    [SerializeField] SpriteRenderer _circleColor;   // 아래 원 서클 

    protected override void Start()
    {
        StartCoroutine(AttackCor());

        _enemyUI = GetComponent<EnemyUI>();
        _hp = _setHp;
        _enemyUI.Init(_hp);

    }

    IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(_attackDelayTime); 


        // 폭탄 터짐 
        Collider[] collider = Physics.OverlapSphere(transform.position, 2f);
        foreach (var item in collider)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.GetComponent<Player>().Hit(_damage,Owner); break;
            }
            else if (item.gameObject.TryGetComponent<Chair>(out Chair chair))
            {
                chair.Hit(_damage, Owner);
            }
        }

        // 흔들림 증가
        ShakeManager.Instance.IncreaseShake(_increaseAmount);

        // 폭파되는 파티클 생성
        GameObject effect = Instantiate(_effectPrefab,transform.position,Quaternion.identity);
        Destroy(effect,2f);
        Destroy(gameObject);

    }

    public override void Hit(float damage, GameObject attacker)
    {
        Hp -= _setHp/2;
        _enemyUI.UpdateHpUI(Hp);
        if (Hp <= 0)
            Die();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
