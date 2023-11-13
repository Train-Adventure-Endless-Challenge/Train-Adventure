using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 _destPos;
    [SerializeField] protected int _speed;
    public float _damage;
    public GameObject Owner { get; set; }



    protected virtual void Start()
    {
        transform.LookAt(_destPos);

        Destroy(gameObject, 3f);
    }

    protected virtual void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.Hit(_damage, Owner);
        }
        else if (other.gameObject.TryGetComponent<Chair>(out Chair chair))
        {
            chair.Hit(_damage, Owner);
        }
        Destroy(gameObject);

    }

    protected  virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
