using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Train : MonoBehaviour
{
    [Header("Variable")]
    [SerializeField] private Animation _frontDoorAnimation;

    [Header("Sound")]
    [SerializeField] private AudioClip _doorOpenSound;
    public GameObject _floor;   // 바닥
    public Transform _playerSpawnPoint;

    public virtual void Start()
    {
        // 동적타임에 NavMesh 생성하기
        NavMeshSurface surfaces = _floor.GetComponent<NavMeshSurface>();

        surfaces.RemoveData();
        surfaces.BuildNavMesh();

        /*surfaces.enabled = false;
        surfaces.enabled = true;*/
    }

    public virtual void Init()
    {

    }

    public void DestroyGameObejct()
    {
        Destroy(gameObject);
    }

    public void OpenDoor()
    {
        _frontDoorAnimation.Play();
        SoundManager.Instance.SFXPlay(_doorOpenSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameManager.Instance.NextStage();
        }
    }
}
