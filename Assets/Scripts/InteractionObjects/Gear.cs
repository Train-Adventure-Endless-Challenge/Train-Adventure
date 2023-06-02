using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private GearMove move;
    private int _acquisitionsGear;
    [SerializeField] private float _speed;
    public int AcquisitionGear
    {
        get { return _acquisitionsGear;}
        set { _acquisitionsGear = value; }
    }

    private void Start()
    {
        move = GetComponentInChildren<GearMove>();
        move.Speed = _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GearSystem.Instance.AddGear(_acquisitionsGear);
            Destroy(gameObject);
        }
    }
}
