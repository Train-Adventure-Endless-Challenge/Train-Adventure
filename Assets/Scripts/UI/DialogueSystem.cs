using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : SceneSingleton<DialogueSystem>
{
    [SerializeField] bool _isShowing;        // text 보여지고 있는 상황
    string[] _dialogueTexts;

    [SerializeField] TMP_Text _dialogueTextUI;
    [SerializeField] GameObject _dialoguePanelObj;

    float _dialogueCharDelay = 0.05f;
    float _dialogueStringDelay = 0.08f;

    void Start()
    {
        _dialoguePanelObj.gameObject.SetActive(false);
    }

    int _textIndex = 0;

    public void StartDialogueText(string[] dialogueText)
    {
        if (_isShowing) return;
        this._dialogueTexts = dialogueText;

        StopAllCoroutines();
        StartCoroutine(StartDialogueCor());
    }

    IEnumerator StartDialogueCor()
    {

        _isShowing = true;
        _dialoguePanelObj.gameObject.SetActive(true);
        _dialogueTextUI.text = "";      // Clear

        while (_textIndex < _dialogueTexts.Length)
        {
            _dialogueTextUI.text = "";      // Clear
            for (int i = 0; i < _dialogueTexts[_textIndex].Length; i++)
            {
                _dialogueTextUI.text += _dialogueTexts[_textIndex][i];
                yield return new WaitForSeconds(_dialogueCharDelay);
            }
            yield return new WaitForSeconds(_dialogueStringDelay);

            _textIndex++;

            yield return new WaitForEndOfFrame();
        }

        _textIndex = 0;
        _dialoguePanelObj.gameObject.SetActive(false);
        _isShowing = false;
    }
}
