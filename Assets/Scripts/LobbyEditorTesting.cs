using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class LobbyEditorTesting : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    private void OnGUI()
    {
        if (!Application.isEditor) return;
        if (GUI.Button(new Rect(new Vector2(0, 0), new Vector2(200, 50)), "Do Auto Testing"))
        {
            timeline.Play();
        }
    }

    #region Auto Name Input

    [SerializeField] private TMP_InputField nameInput;

    public void AddDemoName()
    {
        nameInput.text = "DemoName";
    }

    #endregion
}
