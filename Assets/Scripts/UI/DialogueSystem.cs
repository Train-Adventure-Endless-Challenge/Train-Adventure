using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : SceneSingleton<DialogueSystem>
{
    [SerializeField] bool _isShowing;        // text 보여지고 있는 상황
    string[] _dialogueText;

    [SerializeField] TMP_Text _dialogueTextUI;
    [SerializeField] GameObject _dialoguePanelObj;
    // Start is called before the first frame update
    void Start()
    {
        _dialoguePanelObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int _textIndex = 0;

    public void StartDialogueText(string[] dialogueText)
    {
        if (_isShowing) return;
        this._dialogueText = dialogueText;

        StopAllCoroutines();
        StartCoroutine(StartDialogueCor());
    }

    IEnumerator StartDialogueCor()
    {

        _isShowing = true;
        _dialoguePanelObj.gameObject.SetActive(true);

        while (_textIndex < _dialogueText.Length)
        {

            for (int i = 0; i < _dialogueText[_textIndex].Length; i++)
            {
                _dialogueTextUI.text += _dialogueText[_textIndex][i];
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.08f);

            _textIndex++;
            _dialogueTextUI.text = "";      // Clear

            yield return new WaitForEndOfFrame();
        }

        _textIndex = 0;
        _dialoguePanelObj.gameObject.SetActive(false);
        _isShowing = false;
    }
}
