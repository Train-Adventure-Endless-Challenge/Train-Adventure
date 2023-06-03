using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = int.MaxValue - 8)]
public class ItemData : ScriptableObject
{
    [SerializeField] private int _id;                                                   // 아이템 id  
    [SerializeField] private string _name;                                              // 아이템의 이름
    [SerializeField] private Sprite _itemImage;                                         // 아이템의 이미지
    [SerializeField] private string _description;                                       // 아이템의 설명
    [SerializeField] private int _damage;                                               // 무기에만 사용할 것 같음 (무기의 기본 공격력)
    [SerializeField] private float _range;                                              // 무기에만 사용할 것 같음 (무기의 사거리)
    [SerializeField] private float _attackSpeed;                                        // 무기에만 사용할 것 같음 (무기의 기본 공격속도)

    [SerializeField] private float _additionalHp;                                       // 아이템이 추가 해주는 체력
    [SerializeField] private int _additionalStemina;                                    // 아이템이 추가 해주는 스테미나
    [SerializeField] private float _additionalStrength;                                 // 아이템이 추가해주는 공격력
    [SerializeField] private int _additionalDefense;                                    // 아이템이 추가해주는 방어력
    [SerializeField] private float _additionalAttackSpeed;                              // 아이템이 추가해주는 공격속도
    [SerializeField] private float _additionalSpeed;                                    // 아이템이 추가해주는 이동속도

    [SerializeField] private float _upgradeValueHp;                                     // 아이템 강화 시 늘어나는 체력
    [SerializeField] private int _upgradeValueStemina;                                  // 아이템 강화 시 늘어나는 스테미나
    [SerializeField] private float _upgradeValueStrength;                               // 아이템 강화 시 늘어나는 공격력
    [SerializeField] private int _upgradeValueDefense;                                  // 아이템 강화 시 늘어나는 방어력
    [SerializeField] private float _upgradeValueAttackSpeed;                            // 아이템 강화 시 늘어나는 공격속도
    [SerializeField] private float _upgradeValueSpeed;                                  // 아이템 강화 시 늘어나는 이동속도 

    [SerializeField] private int _maxDurability;                                        // 최대 내구도
    [SerializeField] private int _attackConsumeDurability;                              // 기본 공격 소모 내구도
    [SerializeField] private int _skillConsumeDurability;                               // 스킬 공격 소모 내구도
    [SerializeField] private float _skillCooltime;                                      // 스킬 쿨타임

    [SerializeField] private bool _isStackable = false;
    public int Id { get { return _id; } }
    public string Name { get { return _name; } }
    public Sprite ItemImage { get { return _itemImage; } }
    public string Description { get { return _description; } }
    public int Damage { get { return _damage; } }
    public float Range { get { return _range; } }
    public float AttackSpeed { get { return _attackSpeed; } }

    public float AdditionalHp { get { return _additionalHp; } }
    public int AdditionalStemina { get { return _additionalStemina; } }

    public float AdditionalStrength { get { return _additionalStrength; } }
    public int AdditionalDefense { get { return _additionalDefense; } }

    public float AdditionalAttackSpeed { get { return _additionalAttackSpeed; } }
    public float AdditionalSpeed { get { return _additionalSpeed; } }

    public float UpgradeValueHp { get { return _upgradeValueHp; } }
    public int UpgradeValueStemina { get { return _upgradeValueStemina; } }
    public float UpgradeValueStrength { get { return _upgradeValueStrength; } }
    public int UpgradeValueDefense { get { return _upgradeValueDefense; } }
    public float UpgradeValueAttackSpeed { get { return _upgradeValueAttackSpeed; } }
    public float UpgradeValueSpeed { get { return _upgradeValueSpeed; } }

    public int MaxDurability { get { return _maxDurability; } }

    public int AttackConsumeDurability { get { return _attackConsumeDurability; } }
    public int SkillConsumeDurability { get { return _skillConsumeDurability; } }
    public float SkillCooltime { get { return _skillCooltime; } }
    public bool IsStackable { get { return _isStackable; } }
}
