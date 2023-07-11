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

    float _timer = 0f;

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

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_timer <= 0.5f) return; // 0.5초 뒤에 기어 흡수

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
