using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody _rigid;
    [SerializeField] int _speed;
    public float _damage;

    public GameObject Owner { get; set; }

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
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(_damage, Owner);

            Destroy(gameObject);
        } 
        else if(!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);    
        }

        //else if 추후 벽 인 경우에도 태그를 사용하여 총알 삭제 

    }

}
