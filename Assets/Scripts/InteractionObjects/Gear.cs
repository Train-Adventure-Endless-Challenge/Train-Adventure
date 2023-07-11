using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private GearMove move;
    /// <summary>
    /// 얻는 기어 수
    /// </summary>
    private int _acquisitionsGear; // 얻는 기어 수
    /// <summary>
    /// 기어 스피드
    /// </summary>
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _gearGainSound;

    public int AcquisitionGear
    {
        get { return _acquisitionsGear; }
        set { _acquisitionsGear = value; }
    }

    private void Start()
    {
        move = GetComponentInChildren<GearMove>();
        move.Speed = _speed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GearManager.Instance.AddGear(_acquisitionsGear);
            SoundManager.Instance.SFXPlay(_gearGainSound);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GearManager.Instance._gearTrasureBoxObjsList.Remove(gameObject);
    }
}
