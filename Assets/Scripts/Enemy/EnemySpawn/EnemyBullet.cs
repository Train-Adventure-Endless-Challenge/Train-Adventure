using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 _dir;
    [SerializeField] int _speed;
    public float _damage;

    PlayerManager _player => PlayerManager.Instance;

    public GameObject Owner { get; set; }

    void Start()
    {
        _dir = _player.transform.position + new Vector3(0,1f,0);            // 기존 플레이어의 위치는 바닥에 붙어 있어 총알이 바닥에 먼저 체크 되는 문제로 임시 vector 값을 더해줌
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
}
