using UnityEngine;

public class ShakeMachine : InteractionObject
{
    Animator _anim;

    [Header("Sound")]
    [SerializeField] private AudioClip _shakeMachineOpenSound;  // 열리는 효과음
    [SerializeField] private AudioClip _shakeMachineCloseSound; // 닫히는 효과음

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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("Open", true);
            SoundManager.Instance.SFXPlay(_shakeMachineOpenSound);
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
}
