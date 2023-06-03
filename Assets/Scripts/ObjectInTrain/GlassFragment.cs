using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFragment : MonoBehaviour
{

    /// <summary>
    /// 바닥에서 범위 
    /// </summary>
    [SerializeField] GameObject _warningObject;
    void Start()
    {
        Init();
    }

    /// <summary>
    /// 오브젝트 상태 초기화 함수
    /// </summary>
    void Init()
    {
        // 경고 오브젝트 크기 설정
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        _warningObject.transform.localScale = new Vector3(collider.radius * 2, _warningObject.transform.localScale.y, collider.radius * 2);
    }

    /// <summary>
    /// 파편 타격 범위에 들어왔을 때
    /// </summary>
    /// <param name="other">들어온 오브젝트</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 추후 플레이어에 Entity 적용시키면 주석 해제
            //other.GetComponent<Player>().Hit();
        }
    }
}
