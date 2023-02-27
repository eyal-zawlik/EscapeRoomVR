using TMPro;
using UnityEngine;

namespace XRInputFieldKeyboard.Script
{
    public class XRInputFieldEventManager : MonoBehaviour
    {
        [SerializeField] private XRKeyboard keybaord;
        private void Start()
        {
            var fields = FindObjectsOfType<TMP_InputField>(true);
            
            foreach (var tmpInputField in fields)
            {
                tmpInputField.onSelect.AddListener(e =>
                {
                    if(!keybaord.gameObject.activeSelf) keybaord.gameObject.SetActive(true);
                    keybaord.activeInputField = tmpInputField;
                });
            }
        }
    }
}