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
    [SerializeField] private Transform _skillImpactPosition;
    protected override void Init()
    {
        base.Init();
        _weaponCollider = GetComponent<CapsuleCollider>();
    }

    public override void UseActiveSkill()
    {
        base.UseActiveSkill();
        Collider[] colliders = Physics.OverlapSphere(PlayerManager.Instance.gameObject.transform.position, _skillRadius, LayerMask.GetMask(_targetLayer));

        if (colliders.Length <= 0) return;

        Shake();

        foreach (Collider col in colliders)
        {
            Instantiate(_skillEffectPrefab, _skillImpactPosition.position, Quaternion.identity);

            Entity entity = col.gameObject.GetComponentInParent<Entity>();
            entity.Hit(_damage + ((entity.MaxHp - entity.Hp) / 10 * 7), gameObject);
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
            if (_detectionLists.Count == 0)
                SubDurability(itemData.AttackConsumeDurability); // 첫 타격 상대라면 내구도 감소 -> 여러명을 때릴 때 여러 번 감소를 막기위함.

            _detectionLists.Add(collision.gameObject);
            Destroy(Instantiate(_hittingFeelingEffect, collision.contacts[0].thisCollider.transform.position, collision.transform.rotation), 2);
            collision.gameObject.GetComponent<Entity>().Hit(_damage, _playerTransform.gameObject);
        }
    }


    [ContextMenu("a")]
    public override void UpgradeItem()
    {
        base.UpgradeItem();

        _skillCoolTime -= 1;
        _damage += 5;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_skillImpactPosition.position, _skillRadius);
    }
}