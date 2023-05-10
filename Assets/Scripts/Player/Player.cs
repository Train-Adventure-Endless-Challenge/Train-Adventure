// �ۼ��� : ���縸
// �ۼ��� : 2023-05-03

using UnityEngine;

/// <summary>
/// �÷��̾��� �����͸� ��� �ִ� Ŭ����
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ���� �Ǿ� �ִ� �÷��̾� ������ ���� ��
    /// </summary>
    [SerializeField] private PlayerData playerData;

    /// <summary>
    /// �÷��̾��� ���¸� �����ϴ� Enum ��
    /// </summary>
    public PlayerState playerState;

    private float _hp;
    private float _speed;
    private float _damage;
    private float _strength;
    private float _attackSpeed;
    private int _mp;
    private int _defense;

    public float Hp { get { return _hp; } set { _hp = value; } }
    public float Speed { get { return _speed; } set { _speed = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }
    public float Strength { get { return _strength; } set { _strength = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// �����͸� �ʱ�ȭ �ϴ� �Լ�
    /// <br/>
    /// ������ �����ʹ� ������Ƽ�� ���� ���� ����
    /// </summary>
    private void Init()
    {
        _hp = playerData.Hp;
        _speed = playerData.Speed;
        _damage = playerData.Damage;
        _strength = playerData.Strength;
        _attackSpeed = playerData.AttackSpeed;
        _mp = playerData.Mp;
        _defense = playerData.Defense;
    }
}
