using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] _dialogueText;
    [SerializeField] bool _canTalk;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(_canTalk && Input.GetKeyDown(KeyCode.E))     // 상호작용 키 
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canTalk = true;
            _anim.SetBool("talking",true);
            DialogueSystem.Instance.StartDialogueText(_dialogueText);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canTalk = false;
            _anim.SetBool("talking", false);
        }
    }

}
