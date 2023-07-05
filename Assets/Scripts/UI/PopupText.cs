using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private float _destroyTime = 2f;

    private void Awake()
    {
        if (_text == null)
        {
            _text = GetComponentInChildren<TMP_Text>();
        }
    }

    private void Start()
    {
        Destroy(this.gameObject, _destroyTime);
    }

    public void Init(string text)
    {
        _text.text = text;
    }
}
