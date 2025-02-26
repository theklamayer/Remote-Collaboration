using UnityEngine;
using TMPro;

public class ConsoleToText : MonoBehaviour
{
    public TextMeshProUGUI consoleText; // UI-Textfeld für Debug-Ausgabe
    private string logOutput = "";

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logOutput += logString + "\n";
        if (logOutput.Length > 5000) // Verhindert zu lange Logs
        {
            logOutput = logOutput.Substring(logOutput.Length - 4000);
        }
        consoleText.text = logOutput;
    }
}

