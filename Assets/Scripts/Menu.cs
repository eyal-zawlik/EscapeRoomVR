using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Transform playerTrans;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private AudioClip selectionSoundFX;

    private PlayerProfile _profile;

    private void Awake()
    {
        _profile = Resources.Load<PlayerProfile>("Profile");
    }

    public void StartGame()
    {
        // start game
        if (string.IsNullOrEmpty(nameInputField.text)) return;
        _profile.playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", nameInputField.text);

        // change the scene
        SceneManager.LoadScene(1);
    }

    public void PlayUISoundEffect()
    {
        AudioSource.PlayClipAtPoint(selectionSoundFX, playerTrans.position,.6f);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}