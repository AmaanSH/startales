using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCanvas : MonoBehaviour
{
    public static LogCanvas instance;

    public LogEntry entry;
    public GameObject holder;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            gameObject.SetActive(false);
        }
    }

    public static void AddEntry(string character, string text)
    {
        if (instance)
        {
            LogEntry logEntry = Instantiate(instance.entry, instance.holder.transform);
            logEntry.SetText(character, text);
            logEntry.gameObject.SetActive(true);
        }        
    }

    public void View()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
