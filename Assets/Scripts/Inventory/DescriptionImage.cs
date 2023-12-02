using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescriptionImage : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _durabilityText;

    [SerializeField] private Slider _durabilitySlider;

    public void Init(Item item)
    {
        _nameText.text = item.Name;
        _descriptionText.text = item.Description;

        _durabilityText.text = $"내구도\n{item.Durability} / {item.ItemData.MaxDurability}";

        _durabilitySlider.maxValue = item.ItemData.MaxDurability;
        _durabilitySlider.value = item.Durability;
    }
}
