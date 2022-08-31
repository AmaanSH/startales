using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Speech
{
    public Character character;
    public string text;
}

public class DialogueScene : CoreScene
{
    public Speech speech;

    public override IEnumerator Play()
    {
        yield return StartCoroutine(Dialogue.instance.SetText(speech.text, speech.character));
    }
}
