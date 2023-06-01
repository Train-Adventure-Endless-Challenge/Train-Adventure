using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody _rigid;
    [SerializeField] int _speed;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        _rigid.velocity = transform.forward * _speed;
    }


}
