using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Weapon
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private List<GameObject> _detectionLists = new List<GameObject>();


    protected override void Init()
    {
        base.Init();
        _weaponCollider = GetComponent<CapsuleCollider>();
    }

    public override void Attack()
    {
        base.Attack();
        _detectionLists.Clear();
        StartCoroutine(AttackCor());
    }


    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _trailRenderer.enabled = false;
        _weaponCollider.enabled = false;
    }

    public override void AttackColliderOnFunc()
    {
        base.AttackColliderOnFunc();
        _trailRenderer.enabled = true;
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
