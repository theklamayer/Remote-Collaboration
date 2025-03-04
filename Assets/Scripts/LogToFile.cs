using System;
using System.IO;
using UnityEngine;

public class LogToFile : MonoBehaviour
{
    private string logFilePath;

    void Awake()
    {
        logFilePath = Path.Combine(Application.persistentDataPath, "log.txt");

        // Falls die Datei existiert, alten Inhalt löschen (optional)
        File.WriteAllText(logFilePath, "----- Unity Log Start -----\n");
        
        // Unity Logs abfangen und in die Datei schreiben
        Application.logMessageReceived += WriteLog;
    }
    void WriteLog(string logString, string stackTrace, LogType type)
    {
        string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string logEntry = $"{timeStamp} [{type}] {logString}\n";

        File.AppendAllText(logFilePath, logEntry);
    }

    void Start()
    {
        Debug.Log("Test: Logging funktioniert!");
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= WriteLog;
    }
}

