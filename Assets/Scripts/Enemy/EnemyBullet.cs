using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if(Owner != null)
        {
            _rigid.velocity = Owner.transform.forward * _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(_damage, Owner);

            Destroy(gameObject);
        } 

        Destroy(gameObject);    
        //else if 추후 벽 인 경우에도 태그를 사용하여 총알 삭제 

    }

}
