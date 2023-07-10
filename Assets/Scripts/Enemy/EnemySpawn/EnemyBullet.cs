using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 _dir;
    [SerializeField] int _speed;
    public float _damage;
    public GameObject Owner { get; set; }

    void Start()
    {
        transform.LookAt(_dir);

        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Hit(_damage, Owner);

            Destroy(gameObject);
        }
        Destroy(gameObject);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
