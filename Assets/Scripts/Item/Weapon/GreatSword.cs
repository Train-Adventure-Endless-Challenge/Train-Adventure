using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GreatSword : Weapon
{
    [SerializeField] private TrailRenderer _trailRenderer;

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
        yield return new WaitForSeconds(_attackSpeed);
        _trailRenderer.enabled = false;
        _weaponCollider.enabled = false;
    }

    public override void UseActiveSkill()
    {
        base.UseActiveSkill();
        GameObject obj = Instantiate(_skillEffectPrefab, PlayerManager.Instance.gameObject.transform.position + Vector3.up * .5f, Quaternion.identity);
        obj.transform.localScale = Vector3.one * _skillRadius;
        Destroy(obj, 1.5f);

        Collider[] colliders = Physics.OverlapSphere(PlayerManager.Instance.gameObject.transform.position, _skillRadius);

        if (colliders.Length <= 0) return;

        Shake();

        List<GameObject> attackList = new List<GameObject>();
        foreach (Collider col in colliders)
        {
            if (attackList.Contains(col.gameObject)) continue;

            if(col.gameObject.TryGetComponent<Entity>(out Entity entity))
            {
                if (entity.gameObject.CompareTag("Player")) continue;

                entity.Hit(_damage * 1.5f, PlayerManager.Instance.gameObject);
                attackList.Add(col.gameObject);

            }
        }

    }

    public override void SkillEventFunc()
    {
        base.SkillEventFunc();
        UseActiveSkill();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Entity entity) && _detectionLists.Contains(collision.gameObject) == false)
        {
            if (collision.gameObject.CompareTag("Player")) return;
            if (_detectionLists.Count == 0)
                SubDurability(itemData.AttackConsumeDurability); // 첫 타격 상대라면 내구도 감소 -> 여러명을 때릴 때 여러 번 감소를 막기위함.

            _detectionLists.Add(collision.gameObject);
            Destroy(Instantiate(_hittingFeelingEffect, collision.contacts[0].thisCollider.transform.position, collision.transform.rotation), 2);
            collision.gameObject.GetComponent<Entity>().Hit(_damage, _playerTransform.gameObject);
            Shake();
        }
    }

    [ContextMenu("a")]
    public override void UpgradeItem()
    {
        base.UpgradeItem();
        _skillRadius += 0.5f;
        _damage += 5;


    }

    public override void AttackColliderOnFunc()
    {
        base.AttackColliderOnFunc();
        _trailRenderer.enabled = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, _skillRadius);
    }


}

