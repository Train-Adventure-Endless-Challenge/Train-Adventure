using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDangerLine : MonoBehaviour
{
    private TrailRenderer _tr;
    public Vector3 _endPostion;

    private float _speed = 3.5f;

    Vector3 _player;       // 추후 싱글톤으로 받아오기

    private void Awake()
    {
        _tr = GetComponent<TrailRenderer>();

        _tr.startColor = new Color(1, 0, 0, 0.7f);
        _tr.endColor = new Color(1, 0, 0, 0.7f);

        
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform.position;
        _endPostion = _player;
    }


    public void Init(EnemyController enemy)
    {
        transform.position = enemy.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _endPostion,Time.deltaTime * _speed);
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }

}
