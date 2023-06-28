using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dagger : Weapon
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private string _targetLayer = "Enemy";


    private List<GameObject> _detectionLists = new List<GameObject>();

    [SerializeField] private GameObject _skillEffectPrefab;
    [SerializeField] private float _skillRadius;
    [SerializeField] private Transform skillImpactPosition;
    protected override void Init()
    {
        base.Init();
        _weaponCollider = GetComponent<CapsuleCollider>();
    }

    public override void UseActiveSkill()
    {
        base.UseActiveSkill();
        Instantiate(_skillEffectPrefab, skillImpactPosition.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(PlayerManager.Instance.gameObject.transform.position, _skillRadius, LayerMask.GetMask(_targetLayer));
        Debug.Log(colliders.Length);

        if (colliders.Length <= 0) return;

        Shake();

        foreach (Collider col in colliders)
        {
            Debug.Log(col.gameObject.name);
            col.gameObject.GetComponentInParent<Entity>().Hit(_damage * 1.5f, gameObject);
            break;
        }

    }

    public override void SkillEventFunc()
    {
        base.SkillEventFunc();
        UseActiveSkill();
    }

    public override void Attack()
    {
        base.Attack();
        _trailRenderer.enabled = true;
        _weaponCollider.enabled = true;
        _detectionLists.Clear();
        StartCoroutine(AttackCor());
    }

    private IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(_attackSpeed);
        _trailRenderer.enabled = false;
        _weaponCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_targetLayer) && _detectionLists.Contains(collision.gameObject) == false)
        {
            _detectionLists.Add(collision.gameObject);
            Destroy(Instantiate(_hittingFeelingEffect, collision.contacts[0].thisCollider.transform.position, collision.transform.rotation), 2);
            collision.gameObject.GetComponent<Entity>().Hit(_damage, _playerTransform.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(skillImpactPosition.position, _skillRadius);
    }
}