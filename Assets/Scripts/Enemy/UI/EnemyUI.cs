using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{

    EnemyController _enemyController;
    
    [SerializeField] Slider _hpBarSlider;

    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponent<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        _hpBarSlider.value = _enemyController.HP;
    }
}
