using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionImage : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _durabilityText;

    public void Init(string name, string description, string durability)
    {
        _nameText.text = name;
        _descriptionText.text = description;
        _durabilityText.text = durability;
    }
}
