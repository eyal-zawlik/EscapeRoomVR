using System;
using TMPro;
using UnityEngine;

public class XRLogs : MonoBehaviour
{
    [Header("Prefab")] [SerializeField] private GameObject logTextPrefab;

    [Header("Message Colors Indication")] [SerializeField]
    private Color normal;

    [SerializeField] private Color warning;
    [SerializeField] private Color error;

    [Header("References")] [SerializeField] private Transform content;

    private void Awake()
    {
        Application.logMessageReceived += Log;
        Debug.Log("Welcome to XR Log Messages");
    }

    private void Log(string condition, string stacktrace, LogType type)
    {
        if (Application.isEditor) return;
        var title = Instantiate(logTextPrefab, content).GetComponent<TMP_Text>();

        title.color = type switch
        {
            LogType.Error => error,
            LogType.Assert => Color.green,
            LogType.Warning => warning,
            LogType.Log => normal,
            LogType.Exception => error,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        title.fontSize = 12;
        title.text = $"{condition}";
        
        var traces = Instantiate(logTextPrefab, content).GetComponent<TMP_Text>();
        traces.color = Color.white;
        traces.fontSize = 7;
        traces.text = $"{stacktrace}";
    }
}
