using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 _destPos;
    [SerializeField] int _speed;
    public float _damage;
    public GameObject Owner { get; set; }

    void Start()
    {
        transform.LookAt(_destPos);

        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
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

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
