using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody _rigid;
    [SerializeField] int _speed;
    public int _damage;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerHit player = other.gameObject.GetComponent<PlayerHit>();
            player.Hit(_damage);

            Destroy(gameObject);
        }
        //else if 추후 벽 인 경우에도 태그를 사용하여 총알 삭제 
    }

}
