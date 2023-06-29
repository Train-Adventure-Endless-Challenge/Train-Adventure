using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GreatSword : Weapon
{
    [SerializeField] private TrailRenderer _trailRenderer;

    private string _targetLayer = "Enemy";
    [SerializeField] private float _skillRadius;
    
    private List<GameObject> _detectionLists = new List<GameObject>();

    [SerializeField] private GameObject _skillEffectPrefab;
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
        yield return new WaitForSeconds(_attackSpeed / 2);

        _trailRenderer.enabled = true;

        yield return new WaitForSeconds(_attackSpeed / 2);
        _trailRenderer.enabled = false;
        _weaponCollider.enabled = false;
    }

    public override void UseActiveSkill()
    {
        base.UseActiveSkill();
        Instantiate(_skillEffectPrefab, PlayerManager.Instance.gameObject.transform.position + Vector3.up * .5f, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(PlayerManager.Instance.gameObject.transform.position, _skillRadius, LayerMask.GetMask(_targetLayer));

        if (colliders.Length <= 0) return;

        Shake();

        foreach (Collider col in colliders)
        {
            col.gameObject.GetComponentInParent<Entity>().Hit(_damage * 1.5f, gameObject);
        }

    }

    public override void SkillEventFunc()
    {
        base.SkillEventFunc();
        UseActiveSkill();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_targetLayer) && _detectionLists.Contains(collision.gameObject) == false)
        {
            if(_detectionLists.Count == 0)
                SubDurability(itemData.AttackConsumeDurability); // 첫 타격 상대라면 내구도 감소 -> 여러명을 때릴 때 여러 번 감소를 막기위함.

            _detectionLists.Add(collision.gameObject);
            Destroy(Instantiate(_hittingFeelingEffect, collision.contacts[0].thisCollider.transform.position, collision.transform.rotation), 2);
            collision.gameObject.GetComponent<Entity>().Hit(_damage, _playerTransform.gameObject);
            Shake();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, _skillRadius);
    }


}

