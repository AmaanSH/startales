using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LogEntry : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI speechText;
    public Image character;

    public void SetText(string name, string text, Sprite sprite)
    {
        nameText.text = name;
        speechText.text = text;
        character.sprite = sprite;
    }
}
