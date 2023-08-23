using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBomb : MonoBehaviour
{
    [Header("Enemy")]
    float _attackDelayTime = 10f;
    float _increaseAmount = 1f;
    float _smooth = 0.1f;
 
    public float _damage;

    public GameObject Owner { get; set; }

    
    [SerializeField] GameObject _effectPrefab;      // 폭탄이 터지는 effect
    [SerializeField] SpriteRenderer _circleColor;   // 아래 원 서클 

    void Start()
    {
        StartCoroutine(AttackCor());
        StartCoroutine(LerpColor());
        _circleColor.color = Color.white;

    }
    
    private void Update()
    {
    }

    IEnumerator LerpColor()
    {
        float progress = 0; 
        float increment = _smooth / _attackDelayTime; // 적용될 변경사항 값
        while (progress < 1)
        {
            _circleColor.color = Color.Lerp(Color.white, Color.red, progress);
            progress += increment;
            yield return new WaitForSeconds(_smooth);
        }
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
