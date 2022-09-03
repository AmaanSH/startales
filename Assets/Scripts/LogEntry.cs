using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogEntry : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speechText;

    public void SetText(string name, string text)
    {
        nameText.text = name;
        speechText.text = text;
    }
}
