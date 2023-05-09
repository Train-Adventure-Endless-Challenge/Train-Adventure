using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Data", menuName = "Scriptable Object/Item Data",order =int.MaxValue - 8)]
public class ItemData : ScriptableObject
{
    [SerializeField] private int _id;                                                   // ������ id  
    [SerializeField] private string _name;                                              // �������� �̸�
    [SerializeField] private string _description;                                       // �������� ����
    [SerializeField] private int _damage;                                               // ���⿡�� ����� �� ���� (������ �⺻ ���ݷ�)
    [SerializeField] private float _range;                                              // ���⿡�� ����� �� ���� (������ ��Ÿ�)
    [SerializeField] private float _attackSpeed;                                        // ���⿡�� ����� �� ���� (������ �⺻ ���ݼӵ�)

    [SerializeField] private float _additionalHp;                                       // �������� �߰� ���ִ� ü��
    [SerializeField] private int _additionalStemina;                                    // �������� �߰� ���ִ� ���׹̳�
    [SerializeField] private float _additionalStrength;                                 // �������� �߰����ִ� ���ݷ�
    [SerializeField] private int _additionalDefense;                                    // �������� �߰����ִ� ����
    [SerializeField] private float _additionalAttackSpeed;                              // �������� �߰����ִ� ���ݼӵ�
    [SerializeField] private float _additionalSpeed;                                    // �������� �߰����ִ� �̵��ӵ�

    [SerializeField] private float _upgradeValueHp;                                     // ������ ��ȭ �� �þ�� ü��
    [SerializeField] private int _upgradeValueStemina;                                  // ������ ��ȭ �� �þ�� ���׹̳�
    [SerializeField] private float _upgradeValueStrength;                               // ������ ��ȭ �� �þ�� ���ݷ�
    [SerializeField] private int _upgradeValueDefense;                                  // ������ ��ȭ �� �þ�� ����
    [SerializeField] private float _upgradeValueAttackSpeed;                            // ������ ��ȭ �� �þ�� ���ݼӵ�
    [SerializeField] private float _upgradeValueSpeed;                                  // ������ ��ȭ �� �þ�� �̵��ӵ�
    public int Id { get { return _id; } }
    public string Name { get { return _name; } }
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
}
