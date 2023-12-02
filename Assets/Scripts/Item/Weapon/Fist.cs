using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fist : Weapon
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private List<GameObject> _detectionLists = new List<GameObject>();

    private Coroutine _attackCor;

    protected override void Init()
    {
        base.Init();
        _weaponCollider = GetComponent<CapsuleCollider>();
    }

    public override void Attack()
    {
        base.Attack();
        _detectionLists.Clear();
        _attackCor = StartCoroutine(AttackCor());
    }


    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _trailRenderer.enabled = false;
        _weaponCollider.enabled = false;
        _attackCor = null;
    }

    public override void AttackColliderOnFunc()
    {
        base.AttackColliderOnFunc();
        if (_attackCor != null) // 현재 공격 상태라면 trail 활성화
        {
            _trailRenderer.enabled = true;
        }
        else // 현재 공격 상태가 아니라면 공격 trail, collider 비활성화
        {
            _trailRenderer.enabled = false;
            _weaponCollider.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Entity entity) && _detectionLists.Contains(collision.gameObject) == false)
        {
            if (collision.gameObject.CompareTag("Player")) return;

            _detectionLists.Add(collision.gameObject);
            Destroy(Instantiate(_hittingFeelingEffect, collision.contacts[0].thisCollider.transform.position, collision.transform.rotation), 2);
            collision.gameObject.GetComponent<Entity>().Hit(_damage, _playerTransform.gameObject);
            Shake();
        }
    }
}
