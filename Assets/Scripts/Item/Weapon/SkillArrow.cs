using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class SkillArrow : Arrow
{
    [SerializeField] int _reflectCount;

    protected override void Start()
    {
        transform.LookAt(_destPos);
    }

    protected override void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    protected override void OnTriggerEnter(Collider other)
    {
    }

    protected override void OnCollisionEnter(Collision collision)
    {

        Debug.Log("OnCollision: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.Hit(_damage, Owner);
            base.OnCollisionEnter(collision);
        }
        else if (collision.gameObject.TryGetComponent<Chair>(out Chair chair))
        {
            chair.Hit(_damage, Owner);
            base.OnCollisionEnter(collision);
        }

        if (_reflectCount <= 0) base.OnCollisionEnter(collision);
        Reflect(collision);
    }

    public void Reflect(Collision collision)
    {
        Vector3 contact = collision.contacts[0].normal;
        Vector3 dir = (collision.transform.position - transform.position).normalized;

        Vector3 reflectDir = Vector3.Reflect(dir, contact);

        transform.LookAt(transform.position + reflectDir);

        _reflectCount--;
    }
}
