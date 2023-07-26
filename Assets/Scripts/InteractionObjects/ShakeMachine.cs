using UnityEngine;

public class ShakeMachine : InteractionObject
{
    Animator _anim;

    [Header("Sound")]
    [SerializeField] private AudioClip _shakeMachineOpenSound;  // 열리는 효과음
    [SerializeField] private AudioClip _shakeMachineCloseSound; // 닫히는 효과음

    [Header("Direction")]       // 연출을 위한 변수
    [SerializeField] Transform _spawnGearTrans;
    [SerializeField] GameObject _gearObj;       // 기어 모델 object

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    public override void Interact()
    {
        int necessaryGear = (int)ShakeManager.Instance.ShakeAmount * 5;
        if (GearManager.Instance.GearAmount >= necessaryGear)
        {
            ShakeManager.Instance.ClearShake();
            GearManager.Instance.SubGear(necessaryGear);

            RepairAction();
        }
        else
        {
            IngameUIController.Instance.PopupText("기어가 부족합니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("Open", true);
            SoundManager.Instance.SFXPlay(_shakeMachineOpenSound);
        }
        else if (other.CompareTag("Gear"))
        {
            Destroy(other.gameObject,2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("Open", false);
            SoundManager.Instance.SFXPlay(_shakeMachineCloseSound);
        }
    }

    /// <summary>
    /// 기어 수리 시 연출
    /// </summary>
    private void RepairAction()
    {
        //닫히는 연출
        _anim.SetBool("Open", false);
        SoundManager.Instance.SFXPlay(_shakeMachineCloseSound);

        Instantiate(_gearObj,_spawnGearTrans.transform.position, Quaternion.identity);
    }
}
