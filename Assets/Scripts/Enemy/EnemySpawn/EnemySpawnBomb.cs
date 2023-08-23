using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBomb : MonoBehaviour
{
    [Header("Enemy")]
    float _attackDelayTime = 10f;
    float _increaseAmount = 1f;
 
    public float _damage;

    public GameObject Owner { get; set; }

    
    [SerializeField] GameObject _effectPrefab;      // 폭탄이 터지는 effect
    [SerializeField] SpriteRenderer _circleColor;   // 아래 원 서클 
    [SerializeField] 

    void Start()
    {
        StartCoroutine(AttackCor());
        _circleColor.color = Color.white;

    }
    
    private void Update()
    {
        _circleColor.color = Color.Lerp(_circleColor.color, Color.red, 3f);
    }

    IEnumerator AttackCor()
    {
        yield return new WaitForSeconds(_attackDelayTime); 


        // 폭탄 터짐 
        Collider[] collider = Physics.OverlapSphere(transform.position, 2f);
        foreach (var item in collider)
        {
            if (item.gameObject.CompareTag("Player"))
            {
                item.GetComponent<Player>().Hit(_damage,Owner); break;
            }
        }

        // 흔들림 증가
        ShakeManager.Instance.IncreaseShake(_increaseAmount);

        // 폭파되는 파티클 생성
        GameObject effect = Instantiate(_effectPrefab,transform.position,Quaternion.identity);
        Destroy(effect,2f);
        Destroy(gameObject);

    }
}
