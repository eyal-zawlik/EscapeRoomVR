using TMPro;
using UnityEngine;

public class XRKeyboard : MonoBehaviour
{

    public TMP_InputField activeInputField;

    [SerializeField] private TMP_Text[] characters;

    [SerializeField] private DoubleKey[] doubleKeys;
    
    private bool _isUpperCase;

    public void OnKeyPressed(string letter)
    {
        if (activeInputField == null) return;

        activeInputField.text += letter;
    }
    
    public void OnKeyPressed(TMP_Text letter)
    {
        if (activeInputField == null) return;

        activeInputField.text += letter.text;
    }

    public void OnKeyPressed(DoubleKey key)
    {
        if (activeInputField == null) return;
        activeInputField.text += key.CKey;
    }

    public void AddSpace()
    {
        if (activeInputField == null) return;
        activeInputField.text = activeInputField.text + " ";
    }

    public void BackSpace()
    {
        if (activeInputField == null) return;
        if (activeInputField.text.Length == 0) return;
        activeInputField.text = activeInputField.text.Substring(0, activeInputField.text.Length - 1);
    }

    public void ToggleCase()
    {
        _isUpperCase = !_isUpperCase;
        foreach (var letterTmpText in characters)
        {
            letterTmpText.text = _isUpperCase ? letterTmpText.text.ToUpper() : letterTmpText.text.ToLower();
        }
    }

    public void Submit()
    {
        
    }

    public void Clear()
    {
        if (activeInputField == null) return;
        activeInputField.text = "";
    }

    public void ToggleDoubleKeys()
    {
        foreach (var doubleKey in doubleKeys)
        {
            doubleKey.ToggleText();
        }
    }
}
