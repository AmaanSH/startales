using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConsScene : CoreScene
{
    public string consId;
    public bool playPuzzleMusic;
    public GameObject speechHolder;

    public override IEnumerator Play()
    {
        ConstellationManager.SetMode(Mode.Constellation);

        ConstellationManager.StartConsetllation(consId, playPuzzleMusic);

        // play through text
        List<DialogueScene> dialogueScenes = speechHolder.GetComponentsInChildren<DialogueScene>().ToList();
        Dialogue.instance.LoadDialogues(dialogueScenes);

        yield return null;
    }
}
