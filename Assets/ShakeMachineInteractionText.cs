using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShakeMachineInteractionText : MonoBehaviour
{
    [SerializeField] TMP_Text _interactiontext;
    void Start()
    {
        
    }

    void Update()
    {
        _interactiontext.text = $"기어 {ShakeManager.Instance.ShakeAmount * 5}개를 \n사용하여 수리";
    }
}
