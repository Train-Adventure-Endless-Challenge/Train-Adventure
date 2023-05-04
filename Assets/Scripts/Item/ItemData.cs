using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Data", menuName = "Scriptable Object/Item Data",order =int.MaxValue - 8)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _damage; // 무기에만 사용할 것 같음 (무기의 기본 공격력)

    [SerializeField] private float _additionalHp;
    [SerializeField] private int _additionalStemina;

    [SerializeField] private float _additionalStrength;
    [SerializeField] private int _additionalDefense;

    [SerializeField] private float _additionalAttackSpeed;
    [SerializeField] private float _additionalSpeed;
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public int Damage { get { return _damage; } }

    public float AdditionalHp { get { return _additionalHp; } }
    public int AdditionalStemina { get { return _additionalStemina; } }

    public float AdditionalStrength { get { return _additionalStrength; } }
    public int AdditionalDefense { get { return _additionalDefense; } }

    public float AdditionalAttackSpeed { get { return _additionalAttackSpeed; } }
    public float AdditionalSpeed { get { return _additionalSpeed; } }
}
