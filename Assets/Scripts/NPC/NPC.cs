using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] _dialogueText;
    [SerializeField] bool _canTalk;

    private void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        if(_canTalk && Input.GetKeyDown(KeyCode.E))     // 상호작용 키 
        {
            DialogueSystem.Instance.StartDialogueText(_dialogueText);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canTalk = false;
        }
    }

}
