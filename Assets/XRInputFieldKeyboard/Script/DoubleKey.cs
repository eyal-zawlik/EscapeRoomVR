using TMPro;
using UnityEngine;

public class DoubleKey : MonoBehaviour
{
    [SerializeField] private TMP_Text pText;
    [SerializeField] private TMP_Text sText;

    private bool _isPrimary = true;
    
    private void OnValidate()
    {
        pText = transform.GetChild(0).GetComponent<TMP_Text>();
        sText = transform.GetChild(1).GetComponent<TMP_Text>();

        pText.name = "PrimaryKey";
        sText.name = "Secondary";

        name = $"DoubleKey({pText.text}/{sText.text})";
        // sText.enabled = false;
    }

    public string CKey => _isPrimary ? pText.text : sText.text;

    public void ToggleText()
    {
        _isPrimary = !_isPrimary;
        (pText.enabled, sText.enabled) = (_isPrimary, !_isPrimary);
        // (pText.color, sText.color) = (sText.color, pText.color);
    }
}